﻿using FrameWork.BaseRepository;
using Restaurant.DomainModel.ApplicationModel.Beverages;
using Restaurant.DomainModel.ApplicationModel.Food;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccessServiceContract.Repositories
{
    public interface IBeveragesRepository : IBaseRepositorySearchable<Beverages , int , BeveragesSearchModel , BeveragesListItem>
    {
        bool ExistBeveragesName(string BeveragesName);

        bool ExistImage(string Image);

        bool HasRelatedOrders(int BeveragesID);//دارای سفارش مرتبط

		List<BeveragesListItemUI> GetAllListItemInUI();

        bool ExistNameInUpdate(int ID, string Name);

        bool ExistImageInUpdate(int ID, string Image);

        void RemoveImage(int ID);
    }
}
