using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebX.MidWares
{   
    [CheckLogin]
    public class BaseController : Controller
    {

    }
}
