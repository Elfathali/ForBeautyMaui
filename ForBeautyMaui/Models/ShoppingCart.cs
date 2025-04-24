using System;
using System.ComponentModel;

namespace ForBeauty.Models
{
    public class ShoppingCart : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _Id { get; set; }
        private int _ProductId { get; set; }
        private int _UserId { get; set; }
        private string _Name { get; set; }
        private string _Description { get; set; }
        private int _Qyt { get; set; }
        private double _Price { get; set; }
        private double _discount { get; set; }
        private string _ImageUrl { get; set; }
        private string _Size { get; set; }




        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnpropertyChanged(nameof(Id));
            }
        }
        public int ProductId
        {
            get { return _ProductId; }
            set
            {
                _ProductId = value;
                OnpropertyChanged(nameof(ProductId));
            }
        }
        public int UserId
        {
            get { return _UserId; }
            set
            {
                _UserId = value;
                OnpropertyChanged(nameof(UserId));
            }
        }
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnpropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnpropertyChanged(nameof(Description));
            }
        }
        public int Qyt
        {
            get { return _Qyt; }
            set
            {
                _Qyt = value;
                OnpropertyChanged(nameof(Qyt));
            }
        }
        public double price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                OnpropertyChanged(nameof(price));
            }
        }
        public double discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                OnpropertyChanged(nameof(discount));
            }
        }
        public string ImageUrl
        {
            get { return _ImageUrl; }
            set
            {
                _ImageUrl = value;
                OnpropertyChanged(nameof(ImageUrl));
            }
        }
        public string Size
        {
            get { return _Size; }
            set
            {
                _Size = value;
                OnpropertyChanged(nameof(Size));
            }
        }



        public void OnpropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    }
   

}

