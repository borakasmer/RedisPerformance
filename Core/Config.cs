namespace RedisExample.Core
{
    public class Config
    {
        #region Props

        public string RedisEndPoint { get; set; }
        public string RedisPort { get; set; }
        public string RedisPassword { get; set; }
        public int RedisExpireTime { get; set; }
        public string EnvironmentName { get; set; }

        #endregion
        public Config()
        {
        }
    }
}
