using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NosTaleKillList.Models
{
    public class MainViewModel
    {
        public Feinde Feinde { get; set; }
        public Feind SelectedFeind { get; set; }

        public MainViewModel()
        {
            Feinde = new Feinde();
        }


    }
}
