using ECM7.Migrator.Framework;

namespace Core.News.Migrations
{
    [Migration(201110141523)]
    public class Migration_InsertNewsDefaultRecords : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            /*Database.ExecuteNonQuery(
                @"
                 INSERT INTO Pages
                               ([Url]
                               ,[ParentPageId]
                               ,[OrderNumber]
                               ,[UserId]
                               ,[IsServicePage]
                               ,[HideInMainMenu])
                             VALUES ('news/details/{id}'
                                    ,null
                                    ,null
                                    ,null
                                    ,1
                                    ,1)

                INSERT INTO [corev3_4].[dbo].[PageLocales]
                           ([Title]
                           ,[Culture]
                           ,[PageId])
                     VALUES
                           ('News details'
                           ,null
                           ,(SELECT Id From Pages WHERE Pages.Url = 'news/details/{id}'))");*/
        }

        public override void Down()
        {
            Database.ExecuteNonQuery(
                @"
                 DELETE FROM [corev3_4].[dbo].[Pages]
                    WHERE Url='news/details/{id}'
                ");
        }
    }
}
