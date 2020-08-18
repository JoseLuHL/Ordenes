using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XFFurniture.Models
{
    public class MenuApp
    {
        public string MenuTitle
        {
            get;
            set;
        }
        public string MenuDetail
        {
            get;
            set;
        }

        public ImageSource icon
        {
            get;
            set;
        }

        public Page Page
        {
            get;
            set;
        }
    }
}
