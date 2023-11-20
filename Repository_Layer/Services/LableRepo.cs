using Common_Layer.Models;
using Microsoft.AspNetCore.Http;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Services
{
    public class LableRepo : ILableRepo
    {
        private readonly FundoDbContext fundooContext;

        public LableRepo(FundoDbContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public LableEntity AddLable(LableModel model,int noteId,int userId)
        {
            LableEntity entity = new LableEntity();
            entity.Name = model.lableName;
            entity.NoteId= noteId;
            entity.UserId= userId;
            fundooContext.Add(entity);
            var result=fundooContext.SaveChanges();
            if (result > 0)
            {
                return entity;
            }
            else
            {
                return null;
            }

        }
        public List<LableEntity> GetAllLable()
        {

            List<LableEntity> entity = (List<LableEntity>)fundooContext.Lables.ToList();

            return entity;

        } 

        public bool UpdateLable(int lableId,LableModel model,int userId)
        {
            var result=fundooContext.Lables.FirstOrDefault(x=>x.Id==lableId && x.UserId==userId);
            if(result!=null)
            {
               
                result.Name = model.lableName;
                fundooContext.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }


        }

        public bool DeleteLable(int lableId,int userId)
        {
            var result = fundooContext.Lables.FirstOrDefault(x => x.Id == lableId && x.UserId==userId);
            if(result!=null)
            {
                fundooContext.Remove(result);
                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
