<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPRTest1
{
    class Menu
    {
        // Array containing all MenuItems in the Menu
        MenuItem[] menuItems;
        // Maximum size of the Menu
        int size;
        // Index of manuItems that the next MenuItem will be inserted into
        int currentIndex = 0;

        // Creates an empty Menu with the given size
        public Menu(int size)
        {
            this.size = size;
            menuItems = new MenuItem[size];
        }

        // Creates a Menu with the given size containing any MenuItems that are given, as long as they can fit in the given size
        public Menu(int size, params MenuItem[] items)
        {
            this.size = size;
            menuItems = new MenuItem[size];
            if (items.Length > size)
            {
                throw (new Exception("Size must be greater than or equal to the number of items"));
            }
            else
            {
                for (int i = 0; i < items.Length; i++)
                {
                    menuItems[i] = items[i];
                }
            }
            currentIndex = items.Length;
        }

        // Creates a full Menu with given MenuItems
        public Menu(params MenuItem[] items)
        {
            size = items.Length;
            for (int i = 0; i < items.Length; i++)
            {
                menuItems[i] = items[i];
            }
            currentIndex = size;
        }

        // Adds a MenuItem to the next available location in the Menu. Returns false if the Menu is full
        public bool addMenuItem(MenuItem item)
        {
            if (currentIndex < size) {
                menuItems[currentIndex] = item;
                currentIndex += 1;
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPRTest1
{
    class Menu
    {
        // Array containing all MenuItems in the Menu
        MenuItem[] menuItems;
        // Maximum size of the Menu
        int size;
        // Index of manuItems that the next MenuItem will be inserted into
        int currentIndex = 0;

        // Creates an empty Menu with the given size
        public Menu(int size)
        {
            this.size = size;
            menuItems = new MenuItem[size];
        }

        // Creates a Menu with the given size containing any MenuItems that are given, as long as they can fit in the given size
        public Menu(int size, params MenuItem[] items)
        {
            this.size = size;
            menuItems = new MenuItem[size];
            if (items.Length > size)
            {
                throw (new Exception("Size must be greater than or equal to the number of items"));
            }
            else
            {
                for (int i = 0; i < items.Length; i++)
                {
                    menuItems[i] = items[i];
                }
            }
            currentIndex = items.Length;
        }

        // Creates a full Menu with given MenuItems
        public Menu(params MenuItem[] items)
        {
            size = items.Length;
            for (int i = 0; i < items.Length; i++)
            {
                menuItems[i] = items[i];
            }
            currentIndex = size;
        }

        // Adds a MenuItem to the next available location in the Menu. Returns false if the Menu is full
        public bool addMenuItem(MenuItem item)
        {
            if (currentIndex < size) {
                menuItems[currentIndex] = item;
                currentIndex += 1;
                return true;
            }
            else
            {
                return false;
            }
            
        }

        // Sets ensures the menu has constraints (+ve below 10 items)
        public bool menuInRange()
        {
            if ((this.size < 10) && (this.size > 0))
            { 
                return true; 
            }
            else
            {
                return false;
            }
        }

        // TODO: add validation checks for all data processed within this class





    }
}
>>>>>>> master
