using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Tachey001.Models;
using Tachey001.ViewModel;

namespace Tachey001.Repository
{
    public class DapperRepository
    {
        private static string _connectionStr = "data source=bs2021spring.database.windows.net;initial catalog=Tachey;persist security info=True;user id=bs;password=g5gyuv6m*mn@";
        private SqlConnection _con;

        public DapperRepository()
        {
            _con = new SqlConnection(_connectionStr);
        }
        //所有課程
        public List<consoleViewModel> GetCourse()
        {
            string sql = @"SELECT  c.CourseID,c.CustomUrl,c.Title,c.TitlePageImageURL,c.OriginalPrice,c.TotalMinTime,
                                            c.MemberID,m.Photo,AVG(sc.Score)as'AvgScore',
                                            Count(sc.CourseID)as'TotalScore',c.CategoryID,c.CategoryDetailsID,
                                            cd.DetailName,cc.CategoryName,c.CreateDate,COUNT(od.CourseID)as'CountBuyCourse',c.MainClick
        
                                            FROM Course as c
                                            left join Member as m on c.MemberID = m.MemberID
                                            left join CourseScore as sc on c.CourseID = sc.CourseID
                                            left join Order_Detail as od on c.CourseID = od.CourseID
                                            inner join CategoryDetail as cd on c.CategoryDetailsID = cd.DetailID
                                            inner join CourseCategory as cc on c.CategoryID = cc.CategoryID
                                            group by c.CourseID,c.CustomUrl,c.Title,c.TitlePageImageURL,c.OriginalPrice,c.TotalMinTime,
                                                             c.MemberID,c.CategoryID,c.CategoryDetailsID,c.MainClick,c.CreateDate,
                                                             m.Photo,cd.DetailName,cc.CategoryName
                                            ";

            var result = _con.Query<consoleViewModel>(sql).ToList();

            return result;
        }
        //我收藏的課
        public List<consoleViewModel> GetCourse(string MemberId)
        {
            string sql = @"SELECT  c.CourseID,c.CustomUrl,c.Title,c.TitlePageImageURL,c.OriginalPrice,c.TotalMinTime,
                                            c.MemberID,m.Photo,AVG(sc.Score)as'AvgScore',
                                            Count(sc.CourseID)as'TotalScore',c.CategoryID,c.CategoryDetailsID,
                                            cd.DetailName,cc.CategoryName,c.CreateDate,COUNT(od.CourseID)as'CountBuyCourse',c.MainClick
        
                                            FROM Course as c
                                            left join Member as m on c.MemberID = m.MemberID
                                            left join CourseScore as sc on c.CourseID = sc.CourseID
                                            left join Order_Detail as od on c.CourseID = od.CourseID
                                            inner join CategoryDetail as cd on c.CategoryDetailsID = cd.DetailID
                                            inner join CourseCategory as cc on c.CategoryID = cc.CategoryID
                                            inner join Owner as o on c.CourseID = o.CourseID
                                            group by c.CourseID,c.CustomUrl,c.Title,c.TitlePageImageURL,c.OriginalPrice,c.TotalMinTime,
                                                             c.MemberID,c.CategoryID,c.CategoryDetailsID,c.MainClick,c.CreateDate,
                                                             m.Photo,cd.DetailName,cc.CategoryName
                                            ";

            var result = _con.Query<consoleViewModel>(sql).ToList();

            return result;
        }
        //收藏表
        public List<Owner> GetOwners(string MemberId)
        {
            string sql = @"Select * From Owner Where MemberID = @MId";

            var result = _con.Query<Owner>(sql, new { MId = MemberId }).ToList();

            return result;
        }
        //購物車的課
        public List<CartPartialCardViewModel> GetCartPartialViewModel(string MemberId)
        {
            string sql = @"Select 
                                                c.CourseID,
                                                c.CustomUrl,
	                                            c.Title,
	                                            c.TitlePageImageURL,
	                                            c.CreateVerify,
	                                            c.OriginalPrice
                                        From Course as c
                                        inner join ShoppingCart as sc on c.CourseID = sc.CourseID
                                        Where sc.MemberID = @MId
                                    ";

            var result = _con.Query<CartPartialCardViewModel>(sql, new { MId = MemberId }).ToList();
            
            return result;
        }
        //我修的課
    }
}