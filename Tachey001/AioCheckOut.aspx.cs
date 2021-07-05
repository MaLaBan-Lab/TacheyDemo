using ECPay.Payment.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tachey001.Models;


namespace text
{
    public partial class AioCheckOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string product_name = "";
            decimal product_price = 0.0m;
            string product_id = Session["ID"].ToString();
            decimal discount = 0;

            using (TacheyContext _context = new TacheyContext())
            {
                var product = from p in _context.Order_Detail
                              where p.OrderID == product_id
                              select p;
                var count = product.Count();
                var order = _context.Order.Where(x => x.OrderID == product_id).First();
                if (order.TicketID != "0")
                {
                    var ticket = _context.Ticket.Where(x => x.TicketID == order.TicketID).First();
                    discount = (decimal)ticket.Discount;
                }
                else
                    discount = 1;
                
                

               
                foreach (var item in product)
                {
                    //time++;
                    //if (time == count)
                    //{
                    //    product_name += "\""+item.CourseName + "\"" +$"{count}個課程總共:";
                    //}
                    //else
                    //{
                    //    product_name += "\""+item.CourseName +"\""+ "，";
                    //}
                    product_price = product_price + (decimal)item.UnitPrice;



                }
                product_name =  $"{count}個課程總共:";


            }


            var result = product_price;

            List<string> enErrors = new List<string>();

            var AName = product_name;//商品
            var APrice = Decimal.ToInt32(product_price*discount);//單價
            var AQuantity = Int32.Parse("1");//數量
            var ADeduction = Decimal.Parse("1");//折扣

            Console.WriteLine(APrice * AQuantity * ADeduction);
            try
            {
                using (AllInOne oPayment = new AllInOne())
                {
                    oPayment.Send.Items.Add(new Item()
                    {
                        //Name = "蘋果",//商品
                        Name = AName,//商品

                        // Price = Decimal.Parse("100"),//單價
                        Price = APrice,//單價

                        Currency = "新台幣",//幣別
                        //Quantity = Int32.Parse("1"),//數量
                        Quantity = AQuantity,//數量

                        URL = "http://google.com",//商品的說明網址
                        Unit = "個",//單位
                        TaxType = TaxationType.Taxable //商品課稅別
                    });

                    //訂單的商品資料
                    //4311-9522-2222-2222
                    ///服務參數
                    oPayment.MerchantID = "2000132";//ECPay提供的特店編號
                    oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法
                    oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
                    //oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/QueryTradeInfo/V5";//要呼叫介接服務的網址
                   

                    oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
                    oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV


                    ///基本參數

                    //廠商的交易編號
                    oPayment.Send.MerchantTradeNo = Session["ID"].ToString();
                    

                    //廠商的交易時間  格式為：yyyy / MM / dd HH: mm: ss
                    oPayment.Send.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                    //交易總金額      請帶整數，不可有小數點。僅限新台幣。
                    //oPayment.Send.TotalAmount = Decimal.Parse("100");
                    oPayment.Send.TotalAmount = Decimal.Parse(Math.Floor((APrice * AQuantity) * ADeduction).ToString());

                    oPayment.Send.TradeDesc = "交易描述";

                    //付款完成通知回傳的網址(商家)
                    oPayment.Send.ReturnURL = "https://localhost:44394/CheckOutFeedback.aspx";


                  

                    /*選擇支付 Credit:信用卡及銀聯卡(需申請開通) WebATM: 網路 ATM ATM: 自動櫃員機 CVS:超商代碼 BARCODE:超商條碼 
                    ALL:不指定付款方式，由綠界顯示付款方式選擇頁面。*/
                    oPayment.Send.ChoosePayment = PaymentMethod.ALL;

                    //瀏覽器端返回的廠商網址
                    oPayment.Send.ClientBackURL = "https://localhost:44394/CheckOutFeedback.aspx";

                    oPayment.Send.Remark = "";

                    //使用的付款子項目
                    oPayment.Send.ChooseSubPayment = PaymentMethodItem.None;

                    //瀏覽器端回傳付款結果網址(消費者畫面)
                    oPayment.Send.OrderResultURL = "https://localhost:44394/CheckOutFeedback.aspx";

                    //是否需要額外的付款資訊
                    oPayment.Send.NeedExtraPaidInfo = ExtraPaymentInfo.Yes;

                    //來源裝置
                    oPayment.Send.DeviceSource = DeviceType.PC;

                    //不顯示的付款方式
                    //oPayment.Send.PlatformID = "";//特約合作平台商代號
                    oPayment.Send.IgnorePayment = "";




                    /*************************非即時性付款:ATM、CVS 額外功能參數**************/

                    #region ATM 額外功能參數

                    oPayment.SendExtend.ExpireDate = 2;//允許繳費的有效天數
                    //oPayment.SendExtend.PaymentInfoURL = "";//伺服器端回傳付款相關資訊
                    //oPayment.SendExtend.ClientRedirectURL = "";//Client 端回傳付款相關資訊

                    #endregion


                    #region CVS 額外功能參數

                    oPayment.SendExtend.StoreExpireDate = 2; //超商繳費截止時間 CVS:以分鐘為單位 BARCODE:以天為單位
                                                             //oPayment.SendExtend.Desc_1 = "test1";//交易描述 1
                                                             //oPayment.SendExtend.Desc_2 = "test2";//交易描述 2
                                                             //oPayment.SendExtend.Desc_3 = "test3";//交易描述 3
                                                             //oPayment.SendExtend.Desc_4 = "";//交易描述 4
                                                             //oPayment.SendExtend.PaymentInfoURL = "";//伺服器端回傳付款相關資訊
                                                             //oPayment.SendExtend.ClientRedirectURL = "";///Client 端回傳付款相關資訊

                    #endregion

                    /***************************信用卡額外功能參數***************************/

                    #region Credit 功能參數

                    //oPayment.SendExtend.BindingCard = BindingCardType.Yes; 
                    //記憶卡號 使用記憶信用卡 使用：Yes請傳 1不使用：NO請傳 0

                    //oPayment.SendExtend.MerchantMemberID = "Test1234"; //記憶卡號識別碼
                    //oPayment.SendExtend.Language = "ENG"; //語系設定

                    #endregion Credit 功能參數

                    #region 一次付清
                    
                    //oPayment.SendExtend.Redeem = false;   //是否使用紅利折抵
                    //oPayment.SendExtend.UnionPay = true; //是否為銀聯卡交易

                    #endregion

                    #region 分期付款

                    //oPayment.SendExtend.CreditInstallment = "3,6,12";
                    //刷卡分期期數 信用卡分期可用參數為:3,6,12,18,24
                    #endregion 分期付款
                    #region 定期定額

                    //oPayment.SendExtend.PeriodAmount = 1000;//每次授權金額
                    //oPayment.SendExtend.PeriodType = PeriodType.Day;//週期種類
                    //oPayment.SendExtend.Frequency = 1;//執行頻率
                    //oPayment.SendExtend.ExecTimes = 2;//執行次數
                    //oPayment.SendExtend.PeriodReturnURL = "";//伺服器端回傳定期定額的執行結果網址。

                    #endregion


                    /********************* 電子發票開立延伸參數 ********************************/
                    oPayment.Send.InvoiceMark = InvoiceState.Yes; // 指定要開立電子發票
                    oPayment.SendExtend.RelateNumber = "test" + new Random().Next(0, 99999).ToString();//廠商自訂編號                   
                    oPayment.SendExtend.CustomerID = "A12345678";//客戶代號
                    oPayment.SendExtend.CustomerIdentifier = "";//統一編號
                    oPayment.SendExtend.CustomerName = "測試客戶名稱";
                    oPayment.SendExtend.CustomerAddr = "測試客戶地址";
                    oPayment.SendExtend.CustomerPhone = "0912345678";//客戶手機號碼
                    oPayment.SendExtend.CustomerEmail = "test1234560@gmail.com";//客戶電子郵件
                    oPayment.SendExtend.ClearanceMark = CustomsClearance.None;//通關方式
                    oPayment.SendExtend.TaxType = TaxationType.Taxable;//課稅類別
                    oPayment.SendExtend.CarruerType = InvoiceVehicleType.Member;//載具類別
                    oPayment.SendExtend.CarruerNum = "載具編號";
                    oPayment.SendExtend.Donation = DonatedInvoice.No;//捐贈註記
                    oPayment.SendExtend.LoveCode = "";//愛心碼
                    oPayment.SendExtend.Print = PrintFlag.No;//列印註記
                    oPayment.SendExtend.InvoiceRemark = "測試備註";
                    oPayment.SendExtend.DelayDay = Int32.Parse("0");//延遲開立天數
                    oPayment.SendExtend.InvType = TheWordType.General;//字軌類別


                    /* 產生訂單 */
                    enErrors.AddRange(oPayment.CheckOut());//付款結賬
                }
            }
            catch (Exception ex)
            {
                using (TacheyContext _context = new TacheyContext())
                {
                    var o1 = _context.Order.Find(Session["ID"].ToString());
                    o1.OrderStatus = "error";
                    _context.SaveChanges();
                }
                Response.Redirect("~/Pay/Error");
                // 例外錯誤處理。
                enErrors.Add(ex.Message);
              
            }
            finally
            {
                // 顯示錯誤訊息。
                if (enErrors.Count() > 0)
                {
                    // string szErrorMessage = String.Join("\\r\\n", enErrors);
                }
            }


        }
    }
}