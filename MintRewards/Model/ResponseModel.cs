namespace MintRewards.Model
{
     
        public class ResponseModel
        {
            public bool Response { get; set; }
            public string Message { get; set; }
            public object Result { get; set; }
            public string Error { get; set; }
            public ResponseStatusCode StatusCode { get; set; }
        }
        public enum ResponseStatusCode
        {
            Success = 200,
            Failed = 400,
            Exception = 500,
        }
    
}
