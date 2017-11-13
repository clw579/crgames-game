using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPRTest1
{
    class MenuItem
    {
        // Name of the MenuItem
        String name;

        // Creates a MenuItem with given name
        public MenuItem(String itemName)
        {
            name = itemName;
        }

        // Placeholder for derived classes
        public abstract bool handleInput();
    }

    class SaveMenu : MenuItem
    {
        public SaveMenu(String itemName) : base(itemName)
        {
            
        }

        public override bool handleInput()
        {
            return false;
        }
    }

    class LoadMenu : MenuItem
    {
        public LoadMenu(String itemName) : base(itemName)
        {

        }

        public override bool handleInput()
        {
            return false;
        }
    }
}