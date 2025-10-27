namespace Backend.Utility.Dto
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// ステータス
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// メッセージ
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// レスポンスデータ
        /// </summary>
        public T? Result { get; set; }
    }
}
