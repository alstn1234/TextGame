using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace TextGame
{
    class Item
    {
        public int Atk { get; }
        public int Def { get; }
        public string Name { get; }
        public string Dscr { get; }
        public int Price { get; private set; }
        public bool IsEquip { get; set; }
        public Item(string name, string dscr, int price, int atk, int def)
        {
            Name = name;
            Dscr = dscr;
            Price = price;
            Atk = atk;
            Def = def;
            IsEquip = false;
        }
        public virtual Item DeepCopy()
        {
            return this;
        }
        public void Equip()
        {
            IsEquip = true;
        }
        public void UnEquip()
        {
            IsEquip = false;
        }
        public virtual void PrintItemInfo(ref ConsoleTable table, int itemNumber, bool isShop) { }
        public virtual void GetStatus(out int status)
        {
            status = 0;
        }
        public virtual Pack GetTypeEnum()
        {
            return Pack.Unknown;
        }
        public void Buy()
        {
            // Price가 -1이면 구매완료
            Price = -1;
        }
        public virtual void Sell() { }

    }
}
