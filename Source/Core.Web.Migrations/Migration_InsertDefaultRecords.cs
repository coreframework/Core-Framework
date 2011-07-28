using System;
using Core.Web.NHibernate.Helpers;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Static;
using ECM7.Migrator.Framework;
using Migration = ECM7.Migrator.Framework.Migration;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Insert default records.
    /// </summary>
    [Migration(22)]
    public class MigrationInsertDefaultRecords : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            SetupDefaultPageLayoutTemplates();
            SetupDefaultPageLayoutRows();
            SetupDefaultPageLayoutColumns();            
            SetupDefaultRoles();
            SetupDefaultUsers();
        }

        public override void Down()
        {
        }

        private void SetupDefaultPageLayoutTemplates()
        {
            Database.ExecuteNonQuery(
                @"

                 INSERT INTO PageLayoutTemplates
                               ([LayoutCssClass], [Priority])
                             VALUES ('layout-1',1)

                 INSERT INTO PageLayoutTemplates
                               ([LayoutCssClass], [Priority])
                             VALUES ('layout-2',2)

                 INSERT INTO PageLayoutTemplates
                               ([LayoutCssClass], [Priority])
                             VALUES ('layout-3',3)

                 INSERT INTO PageLayoutTemplates
                               ([LayoutCssClass], [Priority])
                             VALUES ('layout-4',4)

                 INSERT INTO PageLayoutTemplates
                               ([LayoutCssClass], [Priority])
                             VALUES ('layout-5',5)
             
                INSERT INTO PageLayoutTemplates
                               ([LayoutCssClass], [Priority])
                             VALUES ('layout-6',6)
                ");
        }

        private void SetupDefaultPageLayoutRows()
        {
            Database.ExecuteNonQuery(
                @"INSERT INTO PageLayoutRows
                               ([TemplateId])
                             VALUES (1)
                INSERT INTO PageLayoutRows
                               ([TemplateId])
                             VALUES (2)
                INSERT INTO PageLayoutRows
                               ([TemplateId])
                             VALUES (3)
                INSERT INTO PageLayoutRows
                               ([TemplateId])
                             VALUES (3)
                INSERT INTO PageLayoutRows
                               ([TemplateId])
                             VALUES (4)
                INSERT INTO PageLayoutRows
                               ([TemplateId])
                             VALUES (4)
                INSERT INTO PageLayoutRows
                               ([TemplateId])
                             VALUES (5)
                INSERT INTO PageLayoutRows
                               ([TemplateId])
                             VALUES (5)
                INSERT INTO PageLayoutRows
                               ([TemplateId])
                             VALUES (5)
                INSERT INTO PageLayoutRows
                               ([TemplateId])
                             VALUES (6)
                INSERT INTO PageLayoutRows
                               ([TemplateId])
                             VALUES (6)
                INSERT INTO PageLayoutRows
                               ([TemplateId])
                             VALUES (6)
                ");
        }

        private void SetupDefaultPageLayoutColumns()
        {
            Database.ExecuteNonQuery(
                @"

                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (1, 100, NULL)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (2, 25, NULL)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (2, 75, NULL)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (3, 100, 2)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (4, 25, NULL)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (4, 75, NULL)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (5, 75, NULL)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (5, 25, NULL)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (6, 100, 2)
                INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (7, 100, 2)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (8, 25, NULL)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (8, 75, NULL)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (9, 100, 2)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (10, 100, 3)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (11, 33, NULL)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (11, 33, NULL)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (11, 33, NULL)
                 INSERT INTO PageLayoutColumns
                               ([RowId], [DefaultWidthValue], [DefaultColspan])
                             VALUES (12, 100, 3)
              
                ");
        }

        private void SetupDefaultRoles()
        {
            Database.ExecuteNonQuery(
                @"

               INSERT INTO Roles
                       ([Name]
                       ,[IsSystemRole]
                       ,[NotAssignableRole]
                       ,[NotPermissible])
                 VALUES ('Administrator', 1, 0, 1)
                
                 INSERT INTO Roles
                       ([Name]
                       ,[IsSystemRole]
                       ,[NotAssignableRole]
                       ,[NotPermissible])
                 VALUES ('Guest', 1, 1, 0)

                 INSERT INTO Roles
                       ([Name]
                       ,[IsSystemRole]
                       ,[NotAssignableRole]
                       ,[NotPermissible])
                 VALUES ('Owner', 1, 1, 1)

                 INSERT INTO Roles
                       ([Name]
                       ,[IsSystemRole]
                       ,[NotAssignableRole]
                       ,[NotPermissible])
                 VALUES ('User', 1, 1, 0)
            ");
        }

        private void SetupDefaultUsers()
        {
            PasswordHash passwordHash = PasswordHelper.Encrypt("admin", PasswordMode.SHA256);
            Database.ExecuteNonQuery(
                String.Format(
                    @"INSERT INTO [Users]
                                   ([Username]
                                   ,[Email]
                                   ,[Hash]
                                   ,[Salt]
                                   ,[EncryptionMode]
                                   ,[Status])
                        VALUES
                            ('admin'
                            ,'admin@admin.com'
                            ,'{0}'
                            ,'{1}'
                            ,{2}
                            ,{3})
                    ",
                    passwordHash.Hash, passwordHash.Salt, (int) PasswordMode.SHA256, (int) UserStatus.Active));

              Database.ExecuteNonQuery(
                String.Format(
                    @"INSERT INTO [UsersToRoles]
                           ([UserId]
                           ,[RoleId])
                     VALUES
                           ((SELECT Id FROM Users WHERE Username='admin')
                           ,{0})",
                    (int) SystemRoles.Administrator));
        }
    }
}
