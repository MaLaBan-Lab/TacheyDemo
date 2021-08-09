using ECPay.Payment.Integration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tachey001.Models;

namespace text
{
    public partial class CheckOutFeedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> enErrors = new List<string>();
            Hashtable htFeedback = null;
            try
            {
                using (AllInOne oPayment = new AllInOne())
                {
                    oPayment.HashKey = "5294y06JbISpM5x9";//ECPay 提供的 HashKey
                    oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay 提供的 HashIV
                    /* 取回付款結果 */
                    enErrors.AddRange(oPayment.CheckOutFeedback(ref htFeedback));
                }
                // 取回所有資料
                if (enErrors.Count() != 0)
                {
                    /* 支付後的回傳的基本參數 */
                    string szMerchantID = "MerchantID";
                    string szMerchantTradeNo = "MerchantTradeNo";
                    string szPaymentDate = "szPaymentDate";
                    string szPaymentType = "szPaymentType";
                    string szPaymentTypeChargeFee = "PaymentTypeChargeFee";
                    string szRtnCode = "RtnCode";
                    string szRtnMsg = "RtnMsg";
                    string szSimulatePaid = "SimulatePaid";
                    string szTradeAmt = "TradeAmt";
                    string szTradeDate = "TradeDate";
                    string szTradeNo = "TradeNo";
                    string szAmount = "szAmount";

                    // 取得資料
                    foreach (string szKey in htFeedback.Keys)
                    {
                        switch (szKey)
                        {
                            /* 支付後的回傳的基本參數 */
                            case "MerchantID": szMerchantID = htFeedback[szKey].ToString(); break;
                            case "MerchantTradeNo": szMerchantTradeNo = htFeedback[szKey].ToString(); break;
                            case "PaymentDate": szPaymentDate = htFeedback[szKey].ToString(); break;
                            case "PaymentType": szPaymentType = htFeedback[szKey].ToString(); break;
                            case "PaymentTypeChargeFee": szPaymentTypeChargeFee = htFeedback[szKey].ToString(); break;
                            case "RtnCode": szRtnCode = htFeedback[szKey].ToString(); break;
                            case "RtnMsg": szRtnMsg = htFeedback[szKey].ToString(); break;
                            case "SimulatePaid": szSimulatePaid = htFeedback[szKey].ToString(); break;
                            case "TradeAmt": szTradeAmt = htFeedback[szKey].ToString(); break;
                            case "TradeDate": szTradeDate = htFeedback[szKey].ToString(); break;
                            case "TradeNo": szTradeNo = htFeedback[szKey].ToString(); break;
                            case "szAmount": szAmount = htFeedback[szKey].ToString(); break;

                            default: break;
                        }
                    }
                }
                else
                {
                    // 其他資料處理。
                }
            }
            catch (Exception ex)
            {
                // 例外錯誤處理。
                enErrors.Add(ex.Message);
            }
            finally
            {
                this.Response.Clear();
                // 回覆成功訊息。
                if (enErrors.Count() == 0)
                {
                    this.Response.Write("1|OK");
                    foreach (DictionaryEntry item in htFeedback)
                    {
                        this.Response.Write(item.Key + " : " + item.Value + "//");
                        Response.Redirect("~/Issue.aspx");
                    }
                }
                // 回覆錯誤訊息。
                else
                {
                    this.Response.Write(String.Format("0|{0}", String.Join("\\r\\n", enErrors)));
                    //using (TacheyContext _context = new TacheyContext())
                    //{
                    //    var o1 = _context.Order.Find(Session["ID"].ToString());
                    //    o1.OrderStatus = "error";
                    //    _context.SaveChanges();
                    //}
                    Response.Redirect("~/Pay/Error");
                }
                this.Response.Flush();
                this.Response.End();
            }
        }
    }

    /*
    信用卡分期支付回傳
    TotalSuccessTimes :                 //已執行成功次數
    TenpayTradeNo :                     //財付通交易編號(目前已無提供此付款方式)
    PeriodType :                        //訂單建立時的所設定的週期種類
    PaymentNo :                         //繳費代碼
    red_dan : 0                         //紅利扣點
    red_yet : 0                         //紅利剩餘點數
    gwsr : 11555974                     //此次所授權的交易單號
    red_ok_amt : 0                      //amt 實際扣款金額
    PeriodAmount :                      //訂單建立時的每次要授權金額
    red_de_amt : 0                      //amt 紅利折抵金額
    SimulatePaid : 0                    //是否為模擬付款
    AlipayTradeNo :                     //支付寶交易編號 (目前已無提供此付款方式)
    WebATMAccBank :                     //付款人銀行代碼
    MerchantID : 2000132                //特店編號
    CustomField4 :                      //自訂名稱欄位4
    AlipayID :                          //付款人在支付寶的系統編號(目前已無提供此付款方式)
    CustomField1 :                      //自訂名稱欄位1
    WebATMAccNo :                       //付款人銀行帳號後五碼
    RtnMsg :                            //交易訊息
    Succeeded PayFrom :                 //【待確認】
    ATMAccBank :                        //付款人銀行代碼
    PaymentType :                       //特店選擇的付款方式
    Credit eci : 0                      //【待確認】信用卡_MasterCard_JCB_VISA
    StoreID :                           //特店旗下店舖代號
    stage : 3                           //分期期數
    MerchantTradeNo : OHwOw997986002    //特店交易編號(由特店提供)
    TradeNo : 2106061321257551          //綠界的交易編號
    card4no : 2222                      //信用卡 卡片的末 4 碼 
    WebATMBankName :                    //銀行名稱
    card6no : 431195                    //信用卡 卡片的前 6 碼
    auth_code : 777777                  //授權碼
    CustomField3 :                      //自訂名稱欄位3 
    PaymentDate : 2021/06/06 13:22:34   //付款時間
    RtnCode : 1                         //交易狀態  1:成功，其餘為失敗
    TradeAmt : 396                      //交易金額
    Frequency :                         //執行頻率
    PaymentTypeChargeFee : 10           //通路費
    process_date : 2021/06/06 13:22:34  //授權成功處理時間
    stast : 132                         //分期頭期金額
    TotalSuccessAmount :                //目前已成功授權的金額合計
    amount : 396                        //本次授權金額 
    CustomField2 :                      //自訂名稱欄位2
    ATMAccNo :                          //WebATMAccNo 付款人銀行帳號後五碼 
    ExecTimes :                         //訂單建立時的所設定的執行頻率
    staed : 132                         //分期各期金額 
    TradeDate : 2021/06/06 13:21:25     //訂單成立時間
    */
}





