﻿namespace ORM_Project.Exceptions
{
    public class OrderAlreadyCancelledException:Exception
    {
        public OrderAlreadyCancelledException(string message):base(message) { }

    }
}
