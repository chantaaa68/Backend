using Backend.Utility.Dto;

namespace Backend.Utility
{
    public class Replace
    {
        /// <summary>
        /// メールボディの変換
        /// ユーザー名、アイテム名、価格を変換してセットする
        /// </summary>
        /// <param name="prm"></param>
        /// <returns></returns>
        public static string TemplateExchange(TemplateExchangeParameter prm)
        {
            //enumsから取得したstringの値が、{{}}ではない件。
            string a = Constant.UserName;
            prm.baseMailBody = prm.baseMailBody
                .Replace(Constant.UserName, prm.userName)
                .Replace(Constant.ItemName, prm.itemName)
                .Replace(Constant.Price, prm.Price);
            return prm.baseMailBody;
        }
    }
}
