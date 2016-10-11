using Auth.Sample.Domain;
using Auth.Sample.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Auth.Sample.Application
{
    public interface IWorkContext
    {
        T_User CurrentUser { get; }


        int CurrentUserId { get; }

        /// <summary>
        /// Get or set value indicating whether we're in admin area
        /// </summary>
        //bool IsAdmin { get; set; }
        //bool IsCurrentUser { get; }

    }
}
