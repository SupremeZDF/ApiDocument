using BT.Manage.Core;
using BT.Manage.Document.AssemblyOperation;
using BT.Manage.Document.Model;
using BT.Manage.Frame.Base.NetCore.ConfigManage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BT.Manage.Tools.Utils;
namespace BT.Manage.Document
{
    public class DocumentServices
    {

        //获取配置的服务名称
        private string ProjectName = JsonConfigMange.GetInstance().AppSettings["ProjectName"];
        private string BaseUrl = JsonConfigMange.GetInstance().AppSettings["BaseUrl"];
        private string BaseReRoute = JsonConfigMange.GetInstance().AppSettings["BaseReroute"];
        /// <summary>
        /// 文档创建
        /// </summary>
        public void CreateDocument()
        {
            AssemblyHandler handler = new AssemblyHandler();
            //获取api接口信息
            AssemblyResult result = new AssemblyResult();
            handler.GetApiDetialInfo(result);
            Console.WriteLine("反射获取信息完毕，开始写入数据库...");
            //数据写入数据库
            WriteInDatabase(result);
        }
        //写入数据库
        public void WriteInDatabase(AssemblyResult result)
        {
            var promodel = db.FindQuery<ProjectInfo>().Where(o => o.FProName == ProjectName).Find().FirstOrDefault();
            List<AssemblyMethodInfo> lsmethodforward = new List<AssemblyMethodInfo>();
            //判断项目是否已存在
            bool isexit = false;
            if (promodel != null && promodel.FID != Guid.Empty)
                isexit = true;
            Guid ProId = Guid.NewGuid();
            string sql = "";
            if (!isexit)
            {
                //写入项目信息
                sql = "insert into d_ProJectInfo values('" + ProId + "','" + ProjectName + "','" + BaseUrl + "','" + DateTime.Now + "','" + BaseReRoute + "');";
            }
            else
                ProId = promodel.FID;
            //删除所有已存在控制器信息
            var framefmodel = db.FindQuery<ControllerInfo>().Where(p => p.FControllerName == "FrameForward" && p.FProId == ProId).Find().FirstOrDefault();
            Guid guid = string.IsNullOrEmpty(framefmodel.FControllerName) ? Guid.NewGuid() : framefmodel.FID;
            db.Delete<ControllerInfo>().Where(p => p.FAddTime < DateTime.Now && p.FProId == ProId && p.FID != guid).Submit();

            db.Delete<MethodInfo>().Where(p => p.FAddTime < DateTime.Now && p.FProId == ProId && p.FConId != guid).Submit();

            var deletefiledsql = @"
                                 DELETE FROM  d_DtoFiledInfo WHERE  FID NOT IN(
                                 SELECT  FID FROM d_MethodInfo WHERE  FProId = '" + ProId + @"' AND  FConId = '" + guid.ToString() + @"'
                                 )
                                AND FProId = '" + ProId + "'  AND FAddTime<'" + DateTime.Now.ToString() + "'";
            db.Excut(deletefiledsql, null).Submit();

            List<BaseQuery> listq = new List<BaseQuery>();
            //写入控制器信息
            foreach (var item in result.ClassList)
            {

                Guid ConId = Guid.NewGuid();
                sql += "insert into d_ControllerInfo values('" + ConId + "','" + item.ClassName.Replace("Controller", "") + "','" + item.ClassDescribe + "','" + DateTime.Now + "','" + ProId + "','" + item.FIsAuthor + "');";
                //添加控制器中方法信息
                if (item.ClasMethList != null)
                {
                    foreach (var mitem in item.ClasMethList)
                    {
                        if (!string.IsNullOrEmpty(mitem.FrameForwardService))
                        {
                            lsmethodforward.Add(mitem);
                        }
                        Guid MethId = Guid.NewGuid();
                        sql += "insert into d_MethodInfo values('" + MethId + "','" + mitem.MethodName + "','" + mitem.MethodDescibe + "','" + mitem.MethodRequestType + "','" + mitem.InJson + "','" + mitem.OutJson + "','" + DateTime.Now + "','" + ConId + "','" + ProId + "','" + mitem.FIsAuthor + "',null);";
                        //添加方法输入字段信息
                        if (mitem.InParamList != null)
                        {
                            foreach (var ifitem in mitem.InParamList)
                            {
                                Guid IFiledId = Guid.NewGuid();
                                sql += "insert into d_DtoFiledInfo values('" + IFiledId + "','" + ifitem.FiledName + "','" + ifitem.FiledDescript + "','" + ifitem.FiledType + "','" + ifitem.FiledInOrOut + "','" + ifitem.FiledLayer + "','" + ifitem.FiledLeft + "','" + ifitem.FiledRight + "','" + DateTime.Now + "','" + MethId + "','" + ProId + "','" + ifitem.FMoudleName + "','" + ifitem.FileClassName + "','" + ifitem.FIsResultRequset + "','" + ifitem.FIsResult + "');";
                            }
                        }
                        //添加方法输出字段信息
                        if (mitem.OutParamList != null)
                        {
                            foreach (var ofitem in mitem.OutParamList)
                            {
                                Guid OFiledId = Guid.NewGuid();
                                sql += "insert into d_DtoFiledInfo values('" + OFiledId + "','" + ofitem.FiledName + "','" + ofitem.FiledDescript + "','" + ofitem.FiledType + "','" + ofitem.FiledInOrOut + "','" + ofitem.FiledLayer + "','" + ofitem.FiledLeft + "','" + ofitem.FiledRight + "','" + DateTime.Now + "','" + MethId + "','" + ProId + "','" + ofitem.FMoudleName + "','" + ofitem.FileClassName + "','" + ofitem.FIsResultRequset + "','" + ofitem.FIsResult + "');";
                            }
                        }
                    }
                }

            }



            if (lsmethodforward.Count > 0)
            {
                //先删除所有注册上去的方法



                //转发地址
                var forwardrouteurl = "";
                var forwardcontrollname = "FrameForward";
                var forwardgetmethod = "get";
                var forwardpostmethod = "post";
                //检索所有已经
                var del_post = "/FrameForward/post/" + BaseReRoute + ".";
                var del_get = "/FrameForward/get/" + BaseReRoute + ".";
                var del_filed_post = @" DELETE FROM d_DtoFiledInfo WHERE FMethId IN(
                                         SELECT FID FROM d_MethodInfo WHERE FForWardURI LIKE '%" + del_post + @"%'
                                        )";
                var del_filed_get = @"DELETE FROM d_DtoFiledInfo WHERE FMethId IN(
                         SELECT FID FROM d_MethodInfo WHERE FForWardURI LIKE '%" + del_get + @"%'
                        )";

                var del_method_post = "DELETE FROM d_MethodInfo WHERE  FForWardURI LIKE '%" + del_post + "%'";
                var del_method_get = "DELETE FROM d_MethodInfo WHERE  FForWardURI LIKE '%" + del_get + "%'";

                db.Excut(del_filed_post, null).Submit();
                db.Excut(del_filed_get, null).Submit();
                db.Excut(del_method_post, null).Submit();
                db.Excut(del_method_get, null).Submit();

                foreach (var item in lsmethodforward)
                {
                    if (item.IsPost)
                    {
                        forwardrouteurl = string.Format("/{0}/{1}/{2}/{3}.{4}.{5}/", item.FrameForwardService, forwardcontrollname, forwardpostmethod, BaseReRoute, item.ControllerName, item.MethodName);
                    }
                    else
                    {
                        forwardrouteurl = string.Format("/{0}/{1}/{2}/{3}.{4}.{5}/", item.FrameForwardService, forwardcontrollname, forwardgetmethod, BaseReRoute, item.ControllerName, item.MethodName);
                    }
                    var apiflag = item.FrameForwardService;

                    var forwardproject = db.FindQuery<ProjectInfo>().Where(p => p.FBaseUrl == apiflag).Find().FirstOrDefault();

                    if (apiflag == "api")
                    {
                        forwardproject = db.FindQuery<ProjectInfo>().Where(p => p.FProName == "消费贷服务").Find().FirstOrDefault();
                    }

                    if (string.IsNullOrEmpty(forwardproject.FProName)) { Console.WriteLine("错误:框架转发配置错误:体系不存在【" + apiflag + "】服务"); return; }
                    var forcontroller = db.FindQuery<ControllerInfo>().Where(p => p.FProId == forwardproject.FID && p.FControllerName == forwardcontrollname).Find().FirstOrDefault();
                    Guid conid;
                    if (string.IsNullOrEmpty(forcontroller.FControllerName))
                    {
                        //创建控制器
                        try
                        {
                            ControllerInfo con = new ControllerInfo();
                            con.FControllerDescibe = "SOA框架转发";
                            con.FControllerName = forwardcontrollname;
                            con.FID = conid = Guid.NewGuid();
                            con.FProId = forwardproject.FID;
                            con.FAddTime = DateTime.Now;
                            string sqlin = "insert into d_ControllerInfo values('" + con.FID + "','" + con.FControllerName + "','" + con.FControllerDescibe + "','" + DateTime.Now + "','" + con.FProId + "','" + 0 + "');";
                            db.Excut(sqlin, null).Submit();

                        }
                        catch (Exception exx)
                        {
                            Console.WriteLine("错误SOA框架转发生成错误:" + exx.Message); return;
                        }

                    }
                    else
                    {
                        conid = forcontroller.FID;
                    }
                    //查看控制器
                    //var method = db.FindQuery<MethodInfo>().Where(p => p.FProId == forwardproject.FID && p.FConId == conid && p.FMethodName == item.MethodName).Find().FirstOrDefault();
                    //if (!string.IsNullOrEmpty(method.FMethodName))
                    //{
                    //    sql += "delete from d_MethodInfo where fid='" + method.FID.ToString() + "';";

                    //}
                    //MethodInfo methodnew = new MethodInfo();
                    Guid MethId = Guid.NewGuid();
                    sql += "insert into d_MethodInfo values('" + MethId + "','" + item.MethodName + "','" + item.MethodDescibe + "','" + item.MethodRequestType + "','" + item.InJson + "','" + item.OutJson + "','" + DateTime.Now + "','" + conid + "','" + forwardproject.FID.ToString() + "','" + item.FIsAuthor + "','" + forwardrouteurl + "');";
                    //添加方法输入字段信息
                    if (item.InParamList != null)
                    {
                        foreach (var ifitem in item.InParamList)
                        {
                            Guid IFiledId = Guid.NewGuid();
                            sql += "insert into d_DtoFiledInfo values('" + IFiledId + "','" + ifitem.FiledName + "','" + ifitem.FiledDescript + "','" + ifitem.FiledType + "','" + ifitem.FiledInOrOut + "','" + ifitem.FiledLayer + "','" + ifitem.FiledLeft + "','" + ifitem.FiledRight + "','" + DateTime.Now + "','" + MethId + "','" + forwardproject.FID.ToString() + "','" + ifitem.FMoudleName + "','" + ifitem.FileClassName + "','" + ifitem.FIsResultRequset + "','" + ifitem.FIsResult + "');";
                        }
                    }
                    //添加方法输出字段信息
                    if (item.OutParamList != null)
                    {
                        foreach (var ofitem in item.OutParamList)
                        {
                            Guid OFiledId = Guid.NewGuid();
                            sql += "insert into d_DtoFiledInfo values('" + OFiledId + "','" + ofitem.FiledName + "','" + ofitem.FiledDescript + "','" + ofitem.FiledType + "','" + ofitem.FiledInOrOut + "','" + ofitem.FiledLayer + "','" + ofitem.FiledLeft + "','" + ofitem.FiledRight + "','" + DateTime.Now + "','" + MethId + "','" + forwardproject.FID.ToString() + "','" + ofitem.FMoudleName + "','" + ofitem.FileClassName + "','" + ofitem.FIsResultRequset + "','" + ofitem.FIsResult + "');";
                        }
                    }
                }

            }
            Console.WriteLine("生成SQL语句成功:" + sql);
            //执行sql语句
            var query = db.Excut(sql, null);

            listq.Add(query);
            var dbs = db.dbscope();
            var bl = dbs.dotrancation((lists, models) =>
            {
                listq.AddInScope(lists);

            }).Submit();
            if (!bl)
            {
                throw dbs.exception;
            }
        }
    }
}
