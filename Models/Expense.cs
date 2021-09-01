using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Expense description")]
        [Required]
        public string ExpenseDescription { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Amount must be greater than 0")]
        public int Amount { get; set; }

        [DisplayName("Expense Category")]
        public int ExpenseCategoryId { get; set; }
        [ForeignKey("ExpenseCategoryId")]
        public virtual ExpenseCategory ExpenseCategory { get; set; }
    }
}
