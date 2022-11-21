namespace StudentAppMvc.Exceptions
{
    public class ObjectExistInDTOException : Exception
    {
        public ObjectExistInDTOException() { }
        public ObjectExistInDTOException(string msg) : base(msg)
        {
        }

        public override string Message
        {
            get
            {
                return "Object exists in another DTO/object";
            }
        }

    }
}
