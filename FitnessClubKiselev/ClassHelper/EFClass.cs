using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessClubKiselev.DB;
using FitnessClubKiselev.Windows;

namespace FitnessClubKiselev.ClassHelper
{
    public class EFClass
    {
        public static Entities1 context { get; set; } = new Entities1();
    }
}
//