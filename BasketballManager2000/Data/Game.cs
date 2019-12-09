using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballManager2000.Data
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }

        [Display(Name = "Game Date")]
        public DateTime GameDate { get; set; }

        public string Venue { get; set; }
        
        [Display(Name = "Amount Paid")]
        public decimal PaidAmount { get; set; }

        [Display(Name = "Paid By Member ID")]
        public int? PaidByMemberId { get; set; }

        [Display(Name = "Paid By")]
        public Member PaidByMember { get; set; }

        public bool IsPaid => PaidAmount > 0;
    }
}
