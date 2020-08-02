using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAC.Controllers.Common
{
    public static class UIHelper
    {
        public static SelectList Build(this SelectList list, string text= "Selecione um Item")
        {
            return new SelectList(list.Items);
            //{

            //}
            //return new SelectList( list.Union(new List<SelectListItem>()
            //{
            //    new SelectListItem(text, "0")
            //}));
            
        }
    }
}
