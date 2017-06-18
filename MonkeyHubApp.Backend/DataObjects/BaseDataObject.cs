using Microsoft.Azure.Mobile.Server;
using System;

namespace MonkeyHubApp.Backend.DataObjects
{
    public class BaseDataObject : EntityData
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}