using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Utilities.SMS
{
    public class SMSSender : ISMSSender
    {
       
        public async Task<bool> SMSSend(string number, string msgbody)
        {

            var url = $"http://portal.metrotel.com.bd/smsapi?" +
                 $"api_key=C20001625e50f2b354c127.63841004&type=text&" +
                 $"contacts={number}&pass=H2bfZIbRFi&senderid=8809612440541&" +
                 $"msg= {msgbody}";


            try
            {
                HttpClient client = new HttpClient();


                var result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
            


        }
    }
}
