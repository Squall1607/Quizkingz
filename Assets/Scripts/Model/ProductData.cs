using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
public class ProductData {
	bool inStock;
	int qty; //so luong
	int priceGem;
	string description;
	string imageTab;
	int id;
	string title;
	List<ImageData> listImage = new List<ImageData>();
	int priceCoin;



	public List<ImageData> ListImg {
		get{ return this.listImage;}
		set{ this.listImage = value;}
	}

	public bool InStock{
		get{ return this.inStock;}
		set{ this.inStock = value;}
	}

	public int QTY{
		get{ return this.qty;}
		set{ this.qty = value;}
	}

	public int PriceGem{
		get{ return this.priceGem;}
		set{ this.priceGem = value;}
	}

	public int ID{
		get{ return this.id;}
		set{ this.id = value;}
	}

	public int PriceCoin{
		get{ return this.priceCoin;}
		set{ this.priceCoin = value;}
	}

	public string Description{
		get{ return this.description;}
		set{ this.description = value;}
	}

	public string ImageTab{
		get{ return this.imageTab;}
		set{ this.imageTab = value;}
	}

	public string Title{
		get{ return this.title;}
		set{ this.title = value;}
	}
}
