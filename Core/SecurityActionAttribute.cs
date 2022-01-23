namespace RedisExample.Core
{
    [AttributeUsage(AttributeTargets.All)]
    public class SecurityActionAttribute : Attribute
    {
        int idSecurityController;
        Int64 actionNumber;
        Int64[] actionNumbers;

        public SecurityActionAttribute(int IdSecurityController, Int64 ActionNumber)
        {
            this.idSecurityController = IdSecurityController;
            this.actionNumber = ActionNumber;
        }
        public SecurityActionAttribute(int IdSecurityController, Int64[] ActionNumbers)
        {
            this.idSecurityController = IdSecurityController;
            this.actionNumbers = ActionNumbers;
        }

        public int IdSecurityController
        {
            get { return idSecurityController; }
        }

        // property to get description 
        public Int64 ActionNumber
        {
            get { return actionNumber; }
        }
    }
}
