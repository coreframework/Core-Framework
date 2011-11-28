using ECM7.Migrator.Framework;

namespace Core.WebContent.Migrations
{
    /// <summary>
    /// Insert default records.
    /// </summary>
    [Migration(12)]
    public class Migration_InsertDefaultRecords : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.ExecuteNonQuery(
                @"
                INSERT INTO [Pages]
                           ([Url]
                           ,[ParentPageId]
                           ,[OrderNumber]
                           ,[UserId]
                           ,[IsServicePage]
                           ,[HideInMainMenu]
                           ,[IsTemplate]
                           ,[TemplateId])
                     VALUES
                           ('web-content/details/{webContentId}'
                           ,NULL
                           ,NULL
                           ,NULL
                           ,1
                           ,0
                           ,0
                           ,NULL)
                ");
            Database.ExecuteNonQuery(
                @"
                INSERT INTO [PageLayouts]
                           ([TemplateId]
                           ,[PageId])
                     VALUES
                           ((SELECT TOP(1) ID FROM PageLayoutTemplates ORDER BY PageLayoutTemplates.Priority)
                           ,(SELECT TOP(1) [Pages].Id FROM [Pages] WHERE [Pages].Url='web-content/details/{webContentId}'))
                ");
            Database.ExecuteNonQuery(
                @"
                INSERT INTO [PageLocales]
                           ([Title]
                           ,[Culture]
                           ,[PageId])
                     VALUES
                           ('Web content details'
                           ,null
                           ,(SELECT TOP(1) [Pages].Id FROM [Pages] WHERE [Pages].Url='web-content/details/{webContentId}'))
                ");
            Database.ExecuteNonQuery(
                @"
                INSERT INTO [WebContent_DetailsWidgets]
                           ([LinkMode])
                     VALUES
                           (1)
                ");
            Database.ExecuteNonQuery(
                @"
                INSERT INTO [PageWidgets]
                           ([InstanceId]
                           ,[ParentWidgetId]
                           ,[PageSection]
                           ,[ColumnNumber]
                           ,[OrderNumber]
                           ,[UserId]
                           ,[PageId]
                           ,[WidgetId]
                           ,[TemplateWidgetId])
                     VALUES
                           ((SELECT TOP(1) [WebContent_DetailsWidgets].Id FROM [WebContent_DetailsWidgets])
                           ,NULL
                           ,1
                           ,1
                           ,1
                           ,NULL
                           ,(SELECT TOP(1) [Pages].Id FROM [Pages] WHERE [Pages].Url='web-content/details/{webContentId}')
                           ,(SELECT TOP(1) [Widgets].Id FROM [Widgets] WHERE [Widgets].Identifier='Core.WebContent.Widgets.ContentDetailsWidget')
                           ,NULL)
                ");
        }

        public override void Revert()
        {
            Database.ExecuteNonQuery(
                @"
                DELETE FROM [Pages]
                    WHERE Url='web-content/details/{webContentId}'
                ");
        }
    }
}
