<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tx1111.aspx.cs" Inherits="ZJPayMentAspNetWebFrom.OneDemo.Tx1111" %>

<!DOCTYPE html>

<%@ Import Namespace="CFCA.Payment.Api" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>模拟商户</title>
    <link rel="stylesheet" type="text/css" href="~/WebRoot/css/Common.css" />
    <link rel="stylesheet" type="text/css" href="~/WebRoot/css/Form.css" />
    <link rel="stylesheet" type="text/css" href="~/WebRoot/css/Table.css" />
    <script type="text/javascript" src="/WebRoot/js/provbank.js"></script>
</head>
    <script language="C#" runat="server">
    String action = "Tx1111.ashx";
    String guid = CFCA.Payment.Utils.CreateNumberByTime();            
</script>

<script language="JavaScript" type="text/JavaScript">
window.onload=function(){
        CreateBank("WebRoot/bank.xml");
}

function doSubmit() {    
    document.form1.submit();
}
</script>

<%
    
    string appPath = Request.ApplicationPath;
    if (!appPath.EndsWith("/"))
    {
        appPath += "/";
    }
 %>
<body>
    <p class="title">
        模拟商户</p>
    <form action="<%=action%>" name="form1" method="post">
    <table width="100%" cellpadding="2" cellspacing="1" border="0" align="center" bgcolor="#DDDDDD">
        <tr>
            <td class="head" height="24" colspan="2">
                商户订单支付（直通车）（1111）
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">
                机构号码
            </td>
            <td width="*" class="content">
                <input type="text" name="InstitutionID" size="40" value="000020" /><必填>
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">
                支付流水号
            </td>
            <td width="*" class="content">
                <input type="text" name="PaymentNo" size="40" value="<%=guid%>" /><必填>
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">
                订单金额
            </td>
            <td width="*" class="content">
                <input type="text" name="Amount" size="40" value="1" /><必填>
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">
                支付服务手续费
            </td>
            <td width="*" class="content">
                <input type="text" name="Fee" size="40" value="0" />
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">
                付款者ID
            </td>
            <td width="*" class="content">
                <input type="text" name="PayerID" size="40" value="" />
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">
                付款者名称
            </td>
            <td width="*" class="content">
                <input type="text" name="PayerName" size="40" value="" />
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">
                结算标识
            </td>
            <td width="*" class="content">
                <input type="text" name="SettlementFlag" size="40" value="0001" /><必填>
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">分账模式</td>
            <td width="*" class="content">
                <select name="SplitType" style="width:274">
                    <option value="10">不分帐</option>
                    <option value="20">按比例分账</option>
                    <option value="30">按金额分账</option>
                </select>
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">
                资金用途
            </td>
            <td width="*" class="content">
                <input type="text" name="Usage" size="40" value="" />
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">
                订单描述
            </td>
            <td width="*" class="content">
                <input type="text" name="Remark" size="40" value="" />
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">
                通知URL
            </td>
            <td width="*" class="content">
                <input type="text" name="NotificationURL" size="40" value="<%=Request.Url.Scheme+"://"+Request.Url.Host+":"+Request.Url.Port+appPath%>ReceiveNoticePage.ashx" />
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">
                银行ID
            </td>
            <td width="*" class="content">
                <select id="BankID" name="BankID" onchange="GetAccountType()" style="width: 274">
                <option value="700">(700)CFCA测试银行</option>
                </select><必填>
            </td>
        </tr>
        <tr class="mouseout">
            <td width="120" class="label" height="24">
                账户类型
            </td>
            <td width="*" class="content">
              <select id="AccountType" name="AccountType" style="width: 274">
              <option value="11">个人账户</option>
              <option value="12">企业账户</option>
              </select><必填>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td height="10" colspan="2" />
        </tr>
        <tr>
            <td width="140" height="28" />
            <td width="*">
                <input type="button" class="ButtonMouseOut" onmouseover="this.className='ButtonMouseOver'"
                    onmouseout="this.className='ButtonMouseOut'" style="width: 60px" value="提交" onclick="doSubmit()"/>
            </td>
        </tr>
    </table>
    <input type="hidden" name="TxCode" value="1111" />
    </form>
</body>
</html>
