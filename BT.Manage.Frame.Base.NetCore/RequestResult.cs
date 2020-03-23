using BT.Manage.Tools.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BT.Manage.Frame.Base
{
    /********************************************************************************
   ** auth： Jason
   ** date： 2016/7/14 15:11:19
   ** desc：
   ** Ver.:  V1.0.0
   ** Copyright (C) 2016 备胎 版权所有。
   *********************************************************************************/

    [JsonObject(MemberSerialization.OptIn)]
    public class ResultRequset : BaseRequestEntity
    {
        [JsonProperty]
        public object @object { get; set; }


        /// <summary>
        /// 可以覆盖此KEY的方式
        /// </summary>
        public virtual string publicApikey { get; set; }
        private string APIKey
        {
            get
            {
                if (string.IsNullOrEmpty((publicApikey)))
                {
                    return GlobKey.globKey;
                }
                else
                {
                    return publicApikey;

                }
            }
        }


        /// <summary>
        /// 开放平台所使用的分配给客户的OPENID
        /// </summary>
        [JsonProperty]
        public string openId
        {

            get;

            set;
        }
        /// <summary>
        /// 获取签名
        /// </summary>
        public override string checkedSign
        {
            get
            {
                var orgin = APIKey + this.time.ToString() + JsonConvert.SerializeObject(@object);
                return EntitySign.To32Md5(orgin);
            }
        }

        /// <summary>
        /// 用户ID 登入人ID
        /// </summary>
        [JsonProperty]
        public Int32 UserID
        {
            
            get;

            set;
        }

        [JsonIgnore]
        public List<Byte[]> attachments { get; set; }

       
        /// <summary>
        /// 签名验证
        /// </summary>
        /// <returns></returns>
        public Result CheckedSign()
        {
            Result r = new Result();
            if (sign == GlobKey.globSkipKey)
            {
                r.code = 1;
                return r;
            }
            if (this.sign == checkedSign)
            {
                r.code = 1;
                return r;
            }
            else
            {
                r.code = 0;
                r.message = "签名验证失败！";
            }
            return r;
        }
    }
}