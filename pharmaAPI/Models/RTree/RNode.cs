using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using pharmaAPI;
using pharmaAPI.Models;

namespace pharmaAPI.Models
{
    public class RNode
    {
        public List<RNode> forward;
        public object content;

        public RNode(List<RNode> cforward, object ccontent)
        {
            forward = cforward;
            content = ccontent;
        }
    }
}