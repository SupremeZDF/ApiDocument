using CFCA.Payment.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZJPayMentAspNetWebFrom
{
    /// <summary>
    /// Tx1111 的摘要说明
    /// </summary>
    public class Tx1111 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            // 1.取得参数
            String institutionID = request.Form["InstitutionID"];
            String paymentNo = request.Form["PaymentNo"];
            long amount = Convert.ToInt64(request.Form["Amount"]);
            long fee = Convert.ToInt64(request.Form["Fee"]);
            String payerID = !request.Form["PayerID"].Equals("") ? request.Form["PayerID"].Trim() : null;
            String payerName = !request.Form["PayerName"].Equals("") ? request.Form["PayerName"].Trim() : null;
            String settlementFlag = !request.Form["SettlementFlag"].Equals("") ? request.Form["SettlementFlag"].Trim() : null;
            String splitType = request.Form["SplitType"];
            String usage = !request.Form["Usage"].Equals("") ? request.Form["Usage"].Trim() : null;
            String remark = !request.Form["Remark"].Equals("") ? request.Form["Remark"].Trim() : null;
            String notificationURL = !request.Form["NotificationURL"].Equals("") ? request.Form["NotificationURL"].Trim() : null;
            String bankID = request.Form["BankID"];
            int accountType = Convert.ToInt32(request.Form["AccountType"]);

            // 2.创建交易请求对象
            Tx1111Request tx1111Request = new Tx1111Request();
            tx1111Request.setInstitutionID(institutionID);
            tx1111Request.setPaymentNo(paymentNo);
            tx1111Request.setAmount(amount);
            tx1111Request.setFee(fee);
            tx1111Request.setPayerID(payerID);
            tx1111Request.setPayerName(payerName);
            tx1111Request.setSettlementFlag(settlementFlag);
            tx1111Request.setSplitType(splitType);
            tx1111Request.setUsage(usage);
            tx1111Request.setRemark(remark);
            tx1111Request.setNotificationURL(notificationURL);
            tx1111Request.setBankID(bankID);
            tx1111Request.setAccountType(accountType);

            // 3.执行报文处理
            tx1111Request.process();

            // 4.将参数放置到request对象
            // 2个交易参数
            HttpContext.Current.Items["message"] = tx1111Request.getRequestMessage();
            HttpContext.Current.Items["signature"] = tx1111Request.getRequestSignature();

            // 1个action(支付平台地址)参数
            HttpContext.Current.Items["action"] = PaymentEnvironment.PaymentURL;

            // 5.转向Request.jsp页面
            context.Server.Transfer("Request.aspx", true);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}