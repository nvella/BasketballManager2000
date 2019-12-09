using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballManager2000.Data
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }

        /// <summary>
        /// User field from ASP.NET Identity database
        /// </summary>
        public string UserId { get; set; }

        public MemberRole Role { get; set; }

        /// <summary>
        /// Navigation for Identity User
        /// </summary>
        public IdentityUser User { get; set; }

        /// <summary>
        /// Navigation for paid for games
        /// </summary>
        public List<Game> PaidForGames { get; set; }
    }

    public enum MemberRole {
        TeamMember,
        TeamManager
    }
}
