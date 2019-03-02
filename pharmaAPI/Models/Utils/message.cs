namespace pharmaAPI
{
    // The point of this class is to provide a json framework to communicate with the api
    class message
    {
        public int status;
        public object body;

        public message(int cstatus, object cbody)
        {
            status = cstatus;
            body = cbody;
        }
    }
}