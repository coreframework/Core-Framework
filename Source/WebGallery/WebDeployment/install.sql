/**********************************************************************/
/* Install.SQL                                                        */
/* Creates a login and makes the user a member of db_owner            */
/*                                                                    */
/**********************************************************************/

-- Declare variables for database name, username and password
DECLARE @dbName sysname,
      @dbUser sysname,
      @dbPwd nvarchar(max);

-- Set variables for database name, username and password
SET @dbName = 'PlaceHolderForDb';
SET @dbUser = 'PlaceHolderForUser';
SET @dbPwd = 'PlaceHolderForPassword';

DECLARE @cmd nvarchar(max)

-- Create login
IF( SUSER_SID(@dbUser) is null )
BEGIN
    print '-- Creating login '
    SET @cmd = N'CREATE LOGIN ' + quotename(@dbUser) + N' WITH PASSWORD ='''+ replace(@dbPwd, '''', '''''') + N''''
    EXEC(@cmd)
END

-- Create database user and map to login
-- and add user to the datareader, datawriter, ddladmin and securityadmin roles
--
SET @cmd = N'USE ' + quotename(@DBName) + N';
IF( NOT EXISTS (SELECT * FROM sys.database_role_members rm INNER JOIN sys.database_principals m ON rm.member_principal_id = m.principal_id WHERE m.NAME = ''' + replace(@dbUser, '''', '''''') + N'''))
BEGIN
    print ''-- Creating user'';
    CREATE USER ' + quotename(@dbUser) + N' FOR LOGIN ' + quotename(@dbUser) + N';
    print ''-- Adding user'';
    EXEC sp_addrolemember ''db_owner'', ''' + replace(@dbUser, '''', '''''') + N''';
END'
EXEC(@cmd)
GO

/* Create a variable to hold dynamic SQL statement */
declare @Select_SQL as nvarchar(500)
 
/* Drop Foreign Key table if exists - this is temporary */
if exists (select * from sysobjects where id = object_id('Foreign_Key_Table') and sysstat & 0xf = 3)
	drop table Foreign_Key_Table
 
select top 1
       object_name(fkeyid) as foreign_key_table, 
       object_name(rkeyid) as primary_key_table,
       object_name(constid) as [name]
Into Foreign_Key_Table from sysforeignkeys where OBJECTPROPERTY(rkeyid, N'IsUserTable') = 1
 
/* Drop foreign key found and get details on next until all have been dropped */
While (select count(*) from foreign_key_table) > 0
 begin
   set @Select_SQL = 'alter table ' + (select foreign_key_table + ' drop ' + name from Foreign_Key_Table)
   exec sp_executesql @Select_SQL   
 
   truncate table Foreign_Key_Table
   Insert into Foreign_Key_Table
   select top 1
          object_name(fkeyid) as foreign_key_table, 
          object_name(rkeyid) as primary_key_table,
          object_name(constid) as [name]
     from sysforeignkeys where OBJECTPROPERTY(rkeyid, N'IsUserTable') = 1
   
 end

-- Drop all tables
exec sp_MSforeachtable "DROP TABLE ? PRINT 'Table ? has been dropped'"

-- variable to object name
declare @name varchar(100)
-- variable to hold object type
declare @xtype char(1)
-- variable to hold sql string
declare @sqlstring nvarchar(1000)

declare SPViews_cursor cursor for
SELECT sysobjects.name, sysobjects.xtype
FROM sysobjects
join sysusers on sysobjects.uid = sysusers.uid
where OBJECTPROPERTY(sysobjects.id, N'IsProcedure') = 1
or OBJECTPROPERTY(sysobjects.id, N'IsView') = 1
UNION
SELECT (SCHEMA_NAME(schema_id) + '.' + name) as name, 'F' as xtype
FROM sys.objects
WHERE type = 'FN'

open SPViews_cursor

fetch next from SPViews_cursor into @name, @xtype

while @@fetch_status = 0
begin
-- test object type if it is a stored procedure
if @xtype = 'P'
begin
set @sqlstring = 'drop procedure ' + @name
exec sp_executesql @sqlstring
set @sqlstring = ' '
end
-- test object type if it is a view
if @xtype = 'V'
begin
set @sqlstring = 'drop view ' + @name
exec sp_executesql @sqlstring
set @sqlstring = ' '
end
-- test object type if it is a function
if @xtype = 'F'
begin
set @sqlstring = 'drop function ' + @name
exec sp_executesql @sqlstring
set @sqlstring = ' '
end

-- get next record
fetch next from SPViews_cursor into @name, @xtype
end

close SPViews_cursor
deallocate SPViews_cursor

GO
/****** Object:  Table [dbo].[Users]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Hash] [nvarchar](255) NOT NULL,
	[Salt] [nvarchar](255) NOT NULL,
	[EncryptionMode] [int] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([Id], [Username], [Email], [Hash], [Salt], [EncryptionMode], [Status]) VALUES (1, N'admin', N'admin@admin.com', N'n1SsQED+MtFVEufQz1IQOt0Ovbh69XukkbJFa7NZOxo=', N'bu5+AIbmB+SPtfHLiDG7X9JOLgHy44mEvpp1JvJZIbM=', 3, 0)
INSERT [dbo].[Users] ([Id], [Username], [Email], [Hash], [Salt], [EncryptionMode], [Status]) VALUES (2, N'testuser', N'testuser@test.com', N'hiDl51NRM8nsW7u8JzCkMnYw8DibSfnZMCCLeLvuuQc=', N'lMZucFKcI13o6t1feBfj80+WzsQlHDR90sU3eFC849U=', 3, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Table [dbo].[LookAndFeelSettings]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LookAndFeelSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LookAndFeelSettings](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BackgroundColor] [nvarchar](7) NULL,
	[FontFamily] [nvarchar](50) NULL,
	[FontSizeValue] [float] NULL,
	[FontSizeUnit] [nvarchar](50) NULL,
	[Color] [nvarchar](7) NULL,
	[Content] [ntext] NULL,
	[WidthValue] [float] NULL,
	[WidthUnit] [nvarchar](50) NULL,
	[HeightValue] [float] NULL,
	[HeightUnit] [nvarchar](50) NULL,
	[OtherStyles] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[LookAndFeelSettings] ON
INSERT [dbo].[LookAndFeelSettings] ([Id], [BackgroundColor], [FontFamily], [FontSizeValue], [FontSizeUnit], [Color], [Content], [WidthValue], [WidthUnit], [HeightValue], [HeightUnit], [OtherStyles]) VALUES (1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[LookAndFeelSettings] ([Id], [BackgroundColor], [FontFamily], [FontSizeValue], [FontSizeUnit], [Color], [Content], [WidthValue], [WidthUnit], [HeightValue], [HeightUnit], [OtherStyles]) VALUES (2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[LookAndFeelSettings] ([Id], [BackgroundColor], [FontFamily], [FontSizeValue], [FontSizeUnit], [Color], [Content], [WidthValue], [WidthUnit], [HeightValue], [HeightUnit], [OtherStyles]) VALUES (3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[LookAndFeelSettings] ([Id], [BackgroundColor], [FontFamily], [FontSizeValue], [FontSizeUnit], [Color], [Content], [WidthValue], [WidthUnit], [HeightValue], [HeightUnit], [OtherStyles]) VALUES (4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[LookAndFeelSettings] ([Id], [BackgroundColor], [FontFamily], [FontSizeValue], [FontSizeUnit], [Color], [Content], [WidthValue], [WidthUnit], [HeightValue], [HeightUnit], [OtherStyles]) VALUES (5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[LookAndFeelSettings] OFF
/****** Object:  Table [dbo].[ListMenuWidgets]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ListMenuWidgets]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ListMenuWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Orientation] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ContentPages]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentPages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContentPages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Content] [ntext] NOT NULL,
	[CreateDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[ContentPages] ON
INSERT [dbo].[ContentPages] ([Id], [Title], [Content], [CreateDate], [LastModifiedDate]) VALUES (1, N'Control Panel', N'<p>Some of the functionality is available only via the administrative control panel. It can be accessed at:</p>
<ul>
    <li>site_url/admin/</li>
    <li>Login: admin</li>
    <li>Password: admin</li>
</ul>
<p>&nbsp;</p>
<p><img width="614" height="414" align="middle" alt="" src="/CoreFramework/Content/UserImages/control_panel.png" /></p>', NULL, NULL)
INSERT [dbo].[ContentPages] ([Id], [Title], [Content], [CreateDate], [LastModifiedDate]) VALUES (2, N'Weather', N'<p><img border="0" src="http://c.gigcount.com/wildfire/IMP/CXNID=2000002.0NXC/bT*xJmx*PTEzMDY1MTEwMzMwNjYmcHQ9MTMwNjUxMTA*MTI1OCZwPTEyMzQ*ODEmZD1BZGRUaGlzXzM2aHJ2MS4xLjAmZz*xJm89/ZWU5YTVkNWE1M2I4NGE3NmEyYWMwMDlkYTZhOWViNDkmb2Y9MA==.gif" style="visibility:hidden;width:0px;height:0px;" alt="" /></p>
<object height="250" width="300" codebase="http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab" id="twc-widget-wrapper-36hour" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000">
<param value="http://s.imwx.com/v.20110211.111141/img/swf/twc-widget-wrapper.swf" name="movie" />
<param value="high" name="quality" />
<param value="#ffffff" name="bgcolor" />
<param value="transparent" name="wmode" />
<param value="always" name="allowScriptAccess" /> <embed height="250" align="middle" width="300" flashvars="site=weather&amp;moduleSrc=http://s.imwx.com/v.20110211.111141/img/swf/twc-widget-36hour.swf&amp;preview=http://s.imwx.com/v.20110211.111159/img/common/twc-widget-node-36hour.png&amp;origin=true&amp;locationId=USTX0057&amp;tempUnits=f&amp;country=US" pluginspage="http://www.adobe.com/go/getflashplayer" type="application/x-shockwave-flash" allowscriptaccess="always" loop="false" play="true" name="twc-widget-wrapper-36hour" wmode="transparent" bgcolor="#ffffff" quality="high" src="http://s.imwx.com/v.20110211.111141/img/swf/twc-widget-wrapper.swf"></embed>
<param value="site=weather&amp;moduleSrc=http://s.imwx.com/v.20110211.111141/img/swf/twc-widget-36hour.swf&amp;preview=http://s.imwx.com/v.20110211.111159/img/common/twc-widget-node-36hour.png&amp;origin=true&amp;locationId=USTX0057&amp;tempUnits=f&amp;country=US" name="FlashVars" /> </object>', NULL, NULL)
INSERT [dbo].[ContentPages] ([Id], [Title], [Content], [CreateDate], [LastModifiedDate]) VALUES (4, N'Where to start?', N'<p><img width="680" height="296" alt="" src="/CoreFramework/Content/UserImages/custom_core_banner.png" /></p>
<p>Hello, Dear visitor!</p>
<p>You are looking at the CORE Framework demo site. Let us guide you through the process of building the site using our open source CORE Framework.&nbsp;</p>
<p>The main idea of that sits behind the framework is a modular approach. The framework can be extended with new modules, that can be setup on the fly. Modules have their custom logic inside and provide widgets, that can be used to present the information for the visitors. Our framework has the mechnism tha allows site administrator building the site from the set of the different widgets.</p>
<p>At the moment the system is configured to work with just one module: Content Pages. Adding some standard CORE features into account such as layout contructor, permission system, users, site navigation it is just enough to build a simple CMS / brochure website.</p>
<p>Try it yourself, loging as testuser/testuser, switch to the edit mode (on the top of the page) and start making you changes!</p>
<p>In case you have any questions, please <a href="http://www.core-frameworks.com/support/contact-us">contact us</a></p>
<p>Yours,<br />
CORE Team</p>', NULL, NULL)
INSERT [dbo].[ContentPages] ([Id], [Title], [Content], [CreateDate], [LastModifiedDate]) VALUES (5, N'Lorem Ipsum', N'<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean posuere egestas nibh ac cursus. Suspendisse venenatis nulla ac magna ultricies nec rhoncus felis lacinia. Curabitur fringilla venenatis dui, ultricies venenatis elit auctor nec. Donec hendrerit faucibus tempus. Quisque sollicitudin aliquam massa eu ornare. Morbi vel velit dolor. Vivamus tellus mi, imperdiet in sollicitudin ut, suscipit eget orci. Donec interdum purus sem, ac pretium nunc. Nullam et gravida ligula. Cras sit amet dolor urna, et blandit est.</p>
<p>Donec neque dui, suscipit ac porttitor quis, sodales vel justo.  Cras malesuada orci a metus rutrum viverra. In at magna sapien. Morbi vitae lorem vel odio semper sagittis ut non purus. Donec ullamcorper fermentum neque, sit amet blandit nulla tristique id. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cras at quam vitae magna rutrum lobortis. Mauris orci urna, feugiat eu varius nec, commodo sed dui. Nullam lacus metus, dictum sed accumsan vel, pretium et tortor. Phasellus ut metus non libero tincidunt tristique nec eu lorem. Donec malesuada suscipit lectus semper dictum. In hac habitasse platea dictumst. Nam rutrum aliquet tempor. Aenean ligula massa, volutpat non sollicitudin sit amet, tristique vitae metus.</p>
<p>Nullam sagittis nisl sit amet quam pellentesque eu suscipit dui elementum. Nunc tempus felis sit amet ipsum malesuada venenatis. Donec mollis pulvinar iaculis. Quisque a dapibus ante. Donec nec quam erat. Duis ut lacus nec velit cursus congue. In leo sapien, interdum et pellentesque eu, vehicula et libero. Mauris et porta erat. In hac habitasse platea dictumst. In porta tortor eu turpis accumsan eget malesuada tortor porttitor. Aenean neque urna, rhoncus ac sagittis quis, dictum commodo neque. Aliquam lectus neque, pretium sed feugiat id, eleifend at neque. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed a arcu libero, a venenatis quam. Aenean velit odio, mattis et venenatis eu, pellentesque at lectus.</p>
<p>Aliquam erat volutpat.  Phasellus eget commodo odio. Sed porta tellus iaculis neque facilisis at rutrum nulla tincidunt. Nulla at risus a tortor viverra venenatis. Nulla viverra lobortis mi, vel pharetra augue tempus et. Nullam convallis, erat ut pharetra luctus, magna lectus imperdiet turpis, vel auctor metus ligula a neque. Nulla luctus euismod adipiscing. Mauris nec interdum risus. Vivamus non mauris sit amet turpis molestie pharetra imperdiet a nulla. Integer lobortis faucibus dolor, quis tristique urna fermentum aliquet.</p>
<p>Nam congue urna vel purus luctus fringilla. Nam imperdiet tellus quis ligula vulputate blandit convallis lacus facilisis. Sed ante ligula, fermentum ac convallis ac, posuere id nisl. Donec eu ipsum magna. Nullam vel felis eu mi commodo euismod. Suspendisse adipiscing, nisl eu malesuada consectetur, odio quam varius libero, eget fringilla nisi mi ac dui. Praesent porttitor odio et lacus pellentesque accumsan eu quis sapien.  Cras ut dui vel leo cursus ullamcorper. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Mauris tempus, mi nec varius rutrum, arcu nunc faucibus massa, vel lacinia felis augue eu enim.</p>
<p>Aenean ac leo sed ligula convallis molestie a egestas leo. Nulla sit amet turpis urna. Praesent vulputate interdum tincidunt. Suspendisse ornare libero in velit fringilla fringilla. Donec non lectus metus, at adipiscing quam. Nullam malesuada, libero at imperdiet pharetra, turpis magna posuere massa, a consequat ipsum erat at libero. Sed ac bibendum urna.</p>', NULL, NULL)
INSERT [dbo].[ContentPages] ([Id], [Title], [Content], [CreateDate], [LastModifiedDate]) VALUES (6, N'Barcelona', N'<iframe width="560" height="349" src="http://www.youtube.com/embed/qsvvUh-fr3o" frameborder="0" allowfullscreen></iframe>', NULL, NULL)
INSERT [dbo].[ContentPages] ([Id], [Title], [Content], [CreateDate], [LastModifiedDate]) VALUES (7, N'Google Map', N'<iframe width="560" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="http://maps.google.com/?ie=UTF8&amp;ll=40.705628,-73.98674&amp;spn=0.070401,0.169086&amp;t=h&amp;z=13&amp;output=embed"></iframe><br /><small><a href="http://maps.google.com/?ie=UTF8&amp;ll=40.705628,-73.98674&amp;spn=0.070401,0.169086&amp;t=h&amp;z=13&amp;source=embed" style="color:#0000FF;text-align:left">View Larger Map</a></small>', NULL, NULL)
SET IDENTITY_INSERT [dbo].[ContentPages] OFF
/****** Object:  Table [dbo].[BreadcrumbsWidgets]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BreadcrumbsWidgets]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BreadcrumbsWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ShowHomePage] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[BreadcrumbsWidgets] ON
INSERT [dbo].[BreadcrumbsWidgets] ([Id], [ShowHomePage]) VALUES (1, 1)
INSERT [dbo].[BreadcrumbsWidgets] ([Id], [ShowHomePage]) VALUES (2, 1)
INSERT [dbo].[BreadcrumbsWidgets] ([Id], [ShowHomePage]) VALUES (3, 1)
SET IDENTITY_INSERT [dbo].[BreadcrumbsWidgets] OFF
/****** Object:  Table [dbo].[EntityTypes]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EntityTypes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[EntityTypes] ON
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (1, N'Core.Web.Areas.Navigation.Widgets.NSiteMapWidget')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (2, N'Core.Web.Areas.Navigation.Widgets.NListMenuWidget')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (3, N'Core.Web.Areas.Navigation.Widgets.NBreadcrumbsWidget')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (4, N'Core.Web.NHibernate.Models.Page')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (5, N'Core.Web.NHibernate.Models.UserGroup')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (6, N'Core.Web.NHibernate.Models.User')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (7, N'Core.Web.NHibernate.Models.Role')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (8, N'Core.Web.NHibernate.Models.Plugin')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (9, N'Core.ContentPages.Widgets.ContentViewerWidget')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (10, N'Core.ContentPages.ContentPagePlugin')
SET IDENTITY_INSERT [dbo].[EntityTypes] OFF
/****** Object:  Table [dbo].[PageLayoutTemplates]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageLayoutTemplates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageLayoutTemplates](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[LayoutCssClass] [nvarchar](255) NOT NULL,
	[Priority] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[PageLayoutTemplates] ON
INSERT [dbo].[PageLayoutTemplates] ([Id], [LayoutCssClass], [Priority]) VALUES (1, N'layout-1', 1)
INSERT [dbo].[PageLayoutTemplates] ([Id], [LayoutCssClass], [Priority]) VALUES (2, N'layout-2', 2)
INSERT [dbo].[PageLayoutTemplates] ([Id], [LayoutCssClass], [Priority]) VALUES (3, N'layout-3', 3)
INSERT [dbo].[PageLayoutTemplates] ([Id], [LayoutCssClass], [Priority]) VALUES (4, N'layout-4', 4)
INSERT [dbo].[PageLayoutTemplates] ([Id], [LayoutCssClass], [Priority]) VALUES (5, N'layout-5', 5)
INSERT [dbo].[PageLayoutTemplates] ([Id], [LayoutCssClass], [Priority]) VALUES (6, N'layout-6', 6)
SET IDENTITY_INSERT [dbo].[PageLayoutTemplates] OFF
/****** Object:  Table [dbo].[UserGroups]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGroups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserGroups](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SchemaInfo]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchemaInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SchemaInfo](
	[Version] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[SchemaInfo] ([Version]) VALUES (1)
INSERT [dbo].[SchemaInfo] ([Version]) VALUES (2)
/****** Object:  Table [dbo].[Roles]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Roles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[IsSystemRole] [bit] NOT NULL,
	[NotAssignableRole] [bit] NOT NULL,
	[NotPermissible] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT [dbo].[Roles] ([Id], [Name], [IsSystemRole], [NotAssignableRole], [NotPermissible]) VALUES (1, N'Administrator', 1, 0, 1)
INSERT [dbo].[Roles] ([Id], [Name], [IsSystemRole], [NotAssignableRole], [NotPermissible]) VALUES (2, N'Guest', 1, 1, 0)
INSERT [dbo].[Roles] ([Id], [Name], [IsSystemRole], [NotAssignableRole], [NotPermissible]) VALUES (3, N'Owner', 1, 1, 1)
INSERT [dbo].[Roles] ([Id], [Name], [IsSystemRole], [NotAssignableRole], [NotPermissible]) VALUES (4, N'User', 1, 1, 0)
INSERT [dbo].[Roles] ([Id], [Name], [IsSystemRole], [NotAssignableRole], [NotPermissible]) VALUES (5, N'Test role', 0, 0, 0)
SET IDENTITY_INSERT [dbo].[Roles] OFF
/****** Object:  Table [dbo].[Plugins]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Plugins]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Plugins](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Identifier] [nvarchar](255) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Version] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Plugins] ON
INSERT [dbo].[Plugins] ([Id], [Identifier], [Status], [CreateDate], [Version], [Title]) VALUES (1, N'5224B396-44E1-11E0-B8AF-801ADFD72185', 1, CAST(0x00009EF0010131CC AS DateTime), NULL, N'Navigation')
INSERT [dbo].[Plugins] ([Id], [Identifier], [Status], [CreateDate], [Version], [Title]) VALUES (2, N'0', 1, CAST(0x00009EF0010131CC AS DateTime), NULL, N'Content Pages')
SET IDENTITY_INSERT [dbo].[Plugins] OFF
/****** Object:  Table [dbo].[Permissions]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permissions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Permissions](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EntityId] [bigint] NULL,
	[Permissions] [bigint] NOT NULL,
	[EntityTypeId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1, 1, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2, 1, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (3, 1, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (4, 1, 15, 9, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (5, 1, 1, 9, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (6, 1, 1, 9, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (7, 2, 15, 9, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (8, 2, 1, 9, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (9, 2, 1, 9, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (10, 3, 15, 9, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (11, 3, 1, 9, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (12, 3, 1, 9, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (13, 2, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (14, 2, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (15, 2, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (16, 4, 15, 9, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (17, 4, 1, 9, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (18, 4, 1, 9, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (19, 3, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (20, 3, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (21, 3, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (22, 5, 15, 9, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (23, 5, 1, 9, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (24, 5, 1, 9, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (25, 4, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (26, 4, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (27, 4, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (28, 6, 15, 9, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (29, 6, 1, 9, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (30, 6, 1, 9, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (31, 7, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (32, 7, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (33, 7, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (34, 8, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (35, 8, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (36, 8, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (37, 9, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (38, 9, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (39, 9, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (40, 10, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (41, 10, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (42, 10, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (43, 11, 15, 9, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (44, 11, 1, 9, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (45, 11, 1, 9, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (46, 12, 15, 9, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (47, 12, 1, 9, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (48, 12, 1, 9, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (49, 13, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (50, 13, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (51, 13, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (52, NULL, 0, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (53, NULL, 0, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (54, NULL, 0, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (55, NULL, 0, 9, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (56, NULL, 0, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (57, NULL, 0, 10, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (58, 5, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (59, 5, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (60, 5, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (61, NULL, 4, 1, 5)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (62, NULL, 4, 2, 5)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (63, NULL, 4, 3, 5)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (64, NULL, 4, 9, 5)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (65, NULL, 16, 4, 5)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (66, NULL, 1, 10, 5)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (67, 6, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (68, 6, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (69, 6, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (70, 14, 15, 9, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (71, 14, 1, 9, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (72, 14, 1, 9, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (73, 7, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (74, 7, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (75, 7, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (76, 8, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (77, 8, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (78, 8, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (79, 8, 0, 4, 1)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (80, 8, 0, 4, 5)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (81, 15, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (82, 15, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (83, 15, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (84, 15, 0, 3, 1)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (85, 15, 4, 3, 5)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (86, 16, 15, 9, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (87, 16, 1, 9, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (88, 16, 1, 9, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (89, 16, 0, 9, 1)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (90, 16, 4, 9, 5)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (91, 9, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (92, 9, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (93, 9, 1, 4, 2)
SET IDENTITY_INSERT [dbo].[Permissions] OFF
/****** Object:  Table [dbo].[Pages]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Pages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Url] [nvarchar](255) NOT NULL,
	[ParentPageId] [bigint] NULL,
	[OrderNumber] [int] NULL,
	[UserId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Pages] ON
INSERT [dbo].[Pages] ([Id], [Title], [Url], [ParentPageId], [OrderNumber], [UserId]) VALUES (1, N'Adminstration', N'adminstration', NULL, 2, 1)
INSERT [dbo].[Pages] ([Id], [Title], [Url], [ParentPageId], [OrderNumber], [UserId]) VALUES (2, N'Home', N'Home', NULL, 1, 1)
INSERT [dbo].[Pages] ([Id], [Title], [Url], [ParentPageId], [OrderNumber], [UserId]) VALUES (3, N'Sample Widgets', N'sample-widgets', NULL, 3, 1)
INSERT [dbo].[Pages] ([Id], [Title], [Url], [ParentPageId], [OrderNumber], [UserId]) VALUES (4, N'Lorem Ipsum', N'lorem-ipsum', NULL, 4, 1)
SET IDENTITY_INSERT [dbo].[Pages] OFF
/****** Object:  Table [dbo].[PageLayoutRows]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageLayoutRows]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageLayoutRows](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TemplateId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[PageLayoutRows] ON
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (1, 1)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (2, 2)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (3, 2)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (4, 3)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (5, 4)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (6, 5)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (7, 5)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (8, 6)
SET IDENTITY_INSERT [dbo].[PageLayoutRows] OFF
/****** Object:  Table [dbo].[ContentPageWidgets]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentPageWidgets]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContentPageWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ContentPageId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[ContentPageWidgets] ON
INSERT [dbo].[ContentPageWidgets] ([Id], [ContentPageId]) VALUES (1, 1)
INSERT [dbo].[ContentPageWidgets] ([Id], [ContentPageId]) VALUES (2, 2)
INSERT [dbo].[ContentPageWidgets] ([Id], [ContentPageId]) VALUES (3, 4)
INSERT [dbo].[ContentPageWidgets] ([Id], [ContentPageId]) VALUES (4, 2)
INSERT [dbo].[ContentPageWidgets] ([Id], [ContentPageId]) VALUES (5, 5)
INSERT [dbo].[ContentPageWidgets] ([Id], [ContentPageId]) VALUES (6, 6)
INSERT [dbo].[ContentPageWidgets] ([Id], [ContentPageId]) VALUES (7, 7)
INSERT [dbo].[ContentPageWidgets] ([Id], [ContentPageId]) VALUES (8, 6)
SET IDENTITY_INSERT [dbo].[ContentPageWidgets] OFF
/****** Object:  Table [dbo].[Migrations]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Migrations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Migrations](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [bigint] NOT NULL,
	[PluginId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Migrations] ON
INSERT [dbo].[Migrations] ([Id], [Version], [PluginId]) VALUES (1, 1, 1)
INSERT [dbo].[Migrations] ([Id], [Version], [PluginId]) VALUES (2, 2, 1)
INSERT [dbo].[Migrations] ([Id], [Version], [PluginId]) VALUES (3, 3, 1)
INSERT [dbo].[Migrations] ([Id], [Version], [PluginId]) VALUES (4, 4, 1)
INSERT [dbo].[Migrations] ([Id], [Version], [PluginId]) VALUES (5, 1, 2)
INSERT [dbo].[Migrations] ([Id], [Version], [PluginId]) VALUES (6, 2, 2)
SET IDENTITY_INSERT [dbo].[Migrations] OFF
/****** Object:  Table [dbo].[UserGroupsToRoles]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGroupsToRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserGroupsToRoles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserGroupId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserGroupsMembers]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGroupsMembers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserGroupsMembers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[UserGroupId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Widgets]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Widgets]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Widgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Identifier] [nvarchar](255) NOT NULL,
	[Status] [int] NOT NULL,
	[Title] [nvarchar](255) NULL,
	[PluginId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Widgets] ON
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [Title], [PluginId]) VALUES (1, N'5224B396-44E1-11E0-B8AF-801ADFD92185', 0, N'Site Map', 1)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [Title], [PluginId]) VALUES (2, N'5224B396-44E1-11E0-B8AF-801ADFD92i85', 0, N'List Menu', 1)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [Title], [PluginId]) VALUES (3, N'4224B396-44E1-11E0-B8AF-801ADFD92185', 0, N'Breadcrumbs', 1)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [Title], [PluginId]) VALUES (4, N'123456', 0, N'Web Content', 2)
SET IDENTITY_INSERT [dbo].[Widgets] OFF
/****** Object:  Table [dbo].[UsersToRoles]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersToRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UsersToRoles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[UsersToRoles] ON
INSERT [dbo].[UsersToRoles] ([Id], [UserId], [RoleId]) VALUES (2, 1, 1)
INSERT [dbo].[UsersToRoles] ([Id], [UserId], [RoleId]) VALUES (3, 2, 5)
SET IDENTITY_INSERT [dbo].[UsersToRoles] OFF
/****** Object:  Table [dbo].[PageWidgets]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageWidgets]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[InstanceId] [bigint] NULL,
	[WidgetIdentifier] [nvarchar](255) NOT NULL,
	[ColumnNumber] [int] NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[UserId] [bigint] NULL,
	[PageId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[PageWidgets] ON
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [WidgetIdentifier], [ColumnNumber], [OrderNumber], [UserId], [PageId]) VALUES (1, 1, N'123456', 1, 2, 1, 1)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [WidgetIdentifier], [ColumnNumber], [OrderNumber], [UserId], [PageId]) VALUES (4, 3, N'123456', 1, 1, 1, 2)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [WidgetIdentifier], [ColumnNumber], [OrderNumber], [UserId], [PageId]) VALUES (5, 4, N'123456', 3, 1, 1, 3)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [WidgetIdentifier], [ColumnNumber], [OrderNumber], [UserId], [PageId]) VALUES (6, 5, N'123456', 1, 2, 1, 4)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [WidgetIdentifier], [ColumnNumber], [OrderNumber], [UserId], [PageId]) VALUES (8, 1, N'4224B396-44E1-11E0-B8AF-801ADFD92185', 1, 1, 1, 4)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [WidgetIdentifier], [ColumnNumber], [OrderNumber], [UserId], [PageId]) VALUES (9, 2, N'4224B396-44E1-11E0-B8AF-801ADFD92185', 1, 1, 1, 1)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [WidgetIdentifier], [ColumnNumber], [OrderNumber], [UserId], [PageId]) VALUES (10, 3, N'4224B396-44E1-11E0-B8AF-801ADFD92185', 1, 1, 1, 3)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [WidgetIdentifier], [ColumnNumber], [OrderNumber], [UserId], [PageId]) VALUES (11, 6, N'123456', 2, 1, 1, 3)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [WidgetIdentifier], [ColumnNumber], [OrderNumber], [UserId], [PageId]) VALUES (12, 7, N'123456', 2, 2, 1, 3)
SET IDENTITY_INSERT [dbo].[PageWidgets] OFF
/****** Object:  Table [dbo].[PageSettings]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageSettings](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomCss] [ntext] NULL,
	[LookAndFeelSettingsId] [bigint] NOT NULL,
	[PageId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[PageSettings] ON
INSERT [dbo].[PageSettings] ([Id], [CustomCss], [LookAndFeelSettingsId], [PageId]) VALUES (1, N'body 
{
    font: 100% Frutiger,"Frutiger Linotype",Univers,Calibri,"Gill Sans","Gill Sans MT","Myriad Pro",Myriad,"DejaVu Sans Condensed","Liberation Sans","Nimbus Sans L",Tahoma,Geneva,"Helvetica Neue",Helvetica,Arial,sans-serif;
    font-size: 12pt; 
    background-color: #ffffff;
    color: #3E483C;
}

p 
{
    margin-top: 5px; 
    margin-bottom:15px;
}

a, a:visited, a:active
{
    color: #930010 !important;
}

.pages-menu li:hover 
{
    background-color:#eeeeee !important;
}', 2, 2)
INSERT [dbo].[PageSettings] ([Id], [CustomCss], [LookAndFeelSettingsId], [PageId]) VALUES (2, N'body 
{
    font: 100% Frutiger,"Frutiger Linotype",Univers,Calibri,"Gill Sans","Gill Sans MT","Myriad Pro",Myriad,"DejaVu Sans Condensed","Liberation Sans","Nimbus Sans L",Tahoma,Geneva,"Helvetica Neue",Helvetica,Arial,sans-serif;
    font-size: 12pt; 
    background-color: #ffffff;
    color: #3E483C;
}

p 
{
    margin-top: 5px; 
    margin-bottom:15px;
}

a, a:visited, a:active
{
    color: #930010 !important;
}

.pages-menu li:hover 
{
    background-color:#eeeeee !important;
}', 3, 1)
INSERT [dbo].[PageSettings] ([Id], [CustomCss], [LookAndFeelSettingsId], [PageId]) VALUES (3, N'body 
{
    font: 100% Frutiger,"Frutiger Linotype",Univers,Calibri,"Gill Sans","Gill Sans MT","Myriad Pro",Myriad,"DejaVu Sans Condensed","Liberation Sans","Nimbus Sans L",Tahoma,Geneva,"Helvetica Neue",Helvetica,Arial,sans-serif;
    font-size: 12pt; 
    background-color: #ffffff;
    color: #3E483C;
}

p 
{
    margin-top: 5px; 
    margin-bottom:15px;
}

a, a:visited, a:active
{
    color: #930010 !important;
}

.pages-menu li:hover 
{
    background-color:#eeeeee !important;
}', 4, 3)
INSERT [dbo].[PageSettings] ([Id], [CustomCss], [LookAndFeelSettingsId], [PageId]) VALUES (4, N'body 
{
    font: 100% Frutiger,"Frutiger Linotype",Univers,Calibri,"Gill Sans","Gill Sans MT","Myriad Pro",Myriad,"DejaVu Sans Condensed","Liberation Sans","Nimbus Sans L",Tahoma,Geneva,"Helvetica Neue",Helvetica,Arial,sans-serif;
    font-size: 12pt; 
    background-color: #ffffff;
    color: #3E483C;
}

p 
{
    margin-top: 5px; 
    margin-bottom:15px;
}

a, a:visited, a:active
{
    color: #930010 !important;
}

.pages-menu li:hover 
{
    background-color:#eeeeee !important;
}', 5, 4)
SET IDENTITY_INSERT [dbo].[PageSettings] OFF
/****** Object:  Table [dbo].[ListMenuWidgetPages]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ListMenuWidgetPages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ListMenuWidgetPages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PageId] [bigint] NOT NULL,
	[ListMenuWidgetId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageLayouts]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageLayouts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageLayouts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TemplateId] [bigint] NOT NULL,
	[PageId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[PageLayouts] ON
INSERT [dbo].[PageLayouts] ([Id], [TemplateId], [PageId]) VALUES (1, 5, 1)
INSERT [dbo].[PageLayouts] ([Id], [TemplateId], [PageId]) VALUES (2, 3, 2)
INSERT [dbo].[PageLayouts] ([Id], [TemplateId], [PageId]) VALUES (3, 2, 3)
INSERT [dbo].[PageLayouts] ([Id], [TemplateId], [PageId]) VALUES (4, 1, 4)
SET IDENTITY_INSERT [dbo].[PageLayouts] OFF
/****** Object:  Table [dbo].[PageLayoutColumns]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageLayoutColumns]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageLayoutColumns](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DefaultWidthValue] [int] NOT NULL,
	[DefaultColspan] [int] NULL,
	[RowId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[PageLayoutColumns] ON
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (1, 100, NULL, 1)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (2, 100, 2, 2)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (3, 25, NULL, 3)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (4, 75, NULL, 3)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (5, 25, NULL, 4)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (6, 75, NULL, 4)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (7, 75, NULL, 5)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (8, 25, NULL, 5)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (9, 100, 2, 6)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (10, 75, NULL, 7)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (11, 25, NULL, 7)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (12, 33, NULL, 8)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (13, 33, NULL, 8)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (14, 33, NULL, 8)
SET IDENTITY_INSERT [dbo].[PageLayoutColumns] OFF
/****** Object:  Table [dbo].[SiteMapWidgets]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SiteMapWidgets]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SiteMapWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Depth] [int] NULL,
	[IncludeRootInTree] [bit] NOT NULL,
	[RootPageId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PageWidgetSettings]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageWidgetSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageWidgetSettings](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomCssClasses] [ntext] NULL,
	[LookAndFeelSettingsId] [bigint] NOT NULL,
	[WidgetId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[PageWidgetSettings] ON
INSERT [dbo].[PageWidgetSettings] ([Id], [CustomCssClasses], [LookAndFeelSettingsId], [WidgetId]) VALUES (1, NULL, 1, 4)
SET IDENTITY_INSERT [dbo].[PageWidgetSettings] OFF
/****** Object:  Table [dbo].[PageLayoutColumnWidthValues]    Script Date: 06/09/2011 16:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageLayoutColumnWidthValues]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PageLayoutColumnWidthValues](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WidthValue] [int] NOT NULL,
	[Colspan] [int] NULL,
	[ColumnId] [bigint] NOT NULL,
	[PageLayoutId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[PageLayoutColumnWidthValues] ON
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (1, 70, NULL, 7, 1)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (2, 30, NULL, 8, 1)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (3, 32, NULL, 5, 1)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (4, 68, NULL, 6, 1)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (5, 100, NULL, 9, 1)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (6, 75, NULL, 10, 1)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (7, 25, NULL, 11, 1)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (8, 33, NULL, 12, 3)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (9, 33, NULL, 13, 3)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (10, 34, NULL, 14, 3)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (11, 100, NULL, 2, 3)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (12, 42, NULL, 3, 3)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (13, 58, NULL, 4, 3)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (14, 25, NULL, 5, 2)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (15, 75, NULL, 6, 2)
SET IDENTITY_INSERT [dbo].[PageLayoutColumnWidthValues] OFF
/****** Object:  Default [DF__Breadcrum__ShowH__68487DD7]    Script Date: 06/09/2011 16:29:10 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Breadcrum__ShowH__68487DD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[BreadcrumbsWidgets]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Breadcrum__ShowH__68487DD7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BreadcrumbsWidgets] ADD  DEFAULT ((0)) FOR [ShowHomePage]
END


End
GO
/****** Object:  Default [DF__SiteMapWi__Inclu__693CA210]    Script Date: 06/09/2011 16:29:10 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__SiteMapWi__Inclu__693CA210]') AND parent_object_id = OBJECT_ID(N'[dbo].[SiteMapWidgets]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__SiteMapWi__Inclu__693CA210]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SiteMapWidgets] ADD  DEFAULT ((0)) FOR [IncludeRootInTree]
END


End
GO
/****** Object:  ForeignKey [FK_ListMenuWidgetPages_ListMenuWidgetPagesPages]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ListMenuWidgetPages_ListMenuWidgetPagesPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[ListMenuWidgetPages]'))
ALTER TABLE [dbo].[ListMenuWidgetPages]  WITH CHECK ADD  CONSTRAINT [FK_ListMenuWidgetPages_ListMenuWidgetPagesPages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ListMenuWidgetPages_ListMenuWidgetPagesPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[ListMenuWidgetPages]'))
ALTER TABLE [dbo].[ListMenuWidgetPages] CHECK CONSTRAINT [FK_ListMenuWidgetPages_ListMenuWidgetPagesPages]
GO
/****** Object:  ForeignKey [FK_ListMenuWidgetPages_ListMenuWidgetPagesWidgets]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ListMenuWidgetPages_ListMenuWidgetPagesWidgets]') AND parent_object_id = OBJECT_ID(N'[dbo].[ListMenuWidgetPages]'))
ALTER TABLE [dbo].[ListMenuWidgetPages]  WITH CHECK ADD  CONSTRAINT [FK_ListMenuWidgetPages_ListMenuWidgetPagesWidgets] FOREIGN KEY([ListMenuWidgetId])
REFERENCES [dbo].[ListMenuWidgets] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ListMenuWidgetPages_ListMenuWidgetPagesWidgets]') AND parent_object_id = OBJECT_ID(N'[dbo].[ListMenuWidgetPages]'))
ALTER TABLE [dbo].[ListMenuWidgetPages] CHECK CONSTRAINT [FK_ListMenuWidgetPages_ListMenuWidgetPagesWidgets]
GO
/****** Object:  ForeignKey [FK_Migrations_MigrationsPlugins]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Migrations_MigrationsPlugins]') AND parent_object_id = OBJECT_ID(N'[dbo].[Migrations]'))
ALTER TABLE [dbo].[Migrations]  WITH CHECK ADD  CONSTRAINT [FK_Migrations_MigrationsPlugins] FOREIGN KEY([PluginId])
REFERENCES [dbo].[Plugins] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Migrations_MigrationsPlugins]') AND parent_object_id = OBJECT_ID(N'[dbo].[Migrations]'))
ALTER TABLE [dbo].[Migrations] CHECK CONSTRAINT [FK_Migrations_MigrationsPlugins]
GO
/****** Object:  ForeignKey [FK_PageLayoutColumns_PageLayoutColumnsRows]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageLayoutColumns_PageLayoutColumnsRows]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageLayoutColumns]'))
ALTER TABLE [dbo].[PageLayoutColumns]  WITH CHECK ADD  CONSTRAINT [FK_PageLayoutColumns_PageLayoutColumnsRows] FOREIGN KEY([RowId])
REFERENCES [dbo].[PageLayoutRows] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageLayoutColumns_PageLayoutColumnsRows]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageLayoutColumns]'))
ALTER TABLE [dbo].[PageLayoutColumns] CHECK CONSTRAINT [FK_PageLayoutColumns_PageLayoutColumnsRows]
GO
/****** Object:  ForeignKey [FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesColumns]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesColumns]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageLayoutColumnWidthValues]'))
ALTER TABLE [dbo].[PageLayoutColumnWidthValues]  WITH CHECK ADD  CONSTRAINT [FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesColumns] FOREIGN KEY([ColumnId])
REFERENCES [dbo].[PageLayoutColumns] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesColumns]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageLayoutColumnWidthValues]'))
ALTER TABLE [dbo].[PageLayoutColumnWidthValues] CHECK CONSTRAINT [FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesColumns]
GO
/****** Object:  ForeignKey [FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesLayouts]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesLayouts]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageLayoutColumnWidthValues]'))
ALTER TABLE [dbo].[PageLayoutColumnWidthValues]  WITH CHECK ADD  CONSTRAINT [FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesLayouts] FOREIGN KEY([PageLayoutId])
REFERENCES [dbo].[PageLayouts] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesLayouts]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageLayoutColumnWidthValues]'))
ALTER TABLE [dbo].[PageLayoutColumnWidthValues] CHECK CONSTRAINT [FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesLayouts]
GO
/****** Object:  ForeignKey [FK_PageLayoutRows_PageLayoutRowsTemplates]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageLayoutRows_PageLayoutRowsTemplates]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageLayoutRows]'))
ALTER TABLE [dbo].[PageLayoutRows]  WITH CHECK ADD  CONSTRAINT [FK_PageLayoutRows_PageLayoutRowsTemplates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[PageLayoutTemplates] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageLayoutRows_PageLayoutRowsTemplates]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageLayoutRows]'))
ALTER TABLE [dbo].[PageLayoutRows] CHECK CONSTRAINT [FK_PageLayoutRows_PageLayoutRowsTemplates]
GO
/****** Object:  ForeignKey [FK_PageLayouts_PageLayoutsPages]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageLayouts_PageLayoutsPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageLayouts]'))
ALTER TABLE [dbo].[PageLayouts]  WITH CHECK ADD  CONSTRAINT [FK_PageLayouts_PageLayoutsPages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageLayouts_PageLayoutsPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageLayouts]'))
ALTER TABLE [dbo].[PageLayouts] CHECK CONSTRAINT [FK_PageLayouts_PageLayoutsPages]
GO
/****** Object:  ForeignKey [FK_PageLayouts_PageLayoutsTemplates]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageLayouts_PageLayoutsTemplates]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageLayouts]'))
ALTER TABLE [dbo].[PageLayouts]  WITH CHECK ADD  CONSTRAINT [FK_PageLayouts_PageLayoutsTemplates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[PageLayoutTemplates] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageLayouts_PageLayoutsTemplates]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageLayouts]'))
ALTER TABLE [dbo].[PageLayouts] CHECK CONSTRAINT [FK_PageLayouts_PageLayoutsTemplates]
GO
/****** Object:  ForeignKey [FK_Pages_PageUsers]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pages_PageUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pages]'))
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_PageUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE SET NULL
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pages_PageUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pages]'))
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_PageUsers]
GO
/****** Object:  ForeignKey [FK_PageSettings_PageSettingsLookAndFeels]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageSettings_PageSettingsLookAndFeels]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageSettings]'))
ALTER TABLE [dbo].[PageSettings]  WITH CHECK ADD  CONSTRAINT [FK_PageSettings_PageSettingsLookAndFeels] FOREIGN KEY([LookAndFeelSettingsId])
REFERENCES [dbo].[LookAndFeelSettings] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageSettings_PageSettingsLookAndFeels]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageSettings]'))
ALTER TABLE [dbo].[PageSettings] CHECK CONSTRAINT [FK_PageSettings_PageSettingsLookAndFeels]
GO
/****** Object:  ForeignKey [FK_PageSettings_PageSettingsPages]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageSettings_PageSettingsPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageSettings]'))
ALTER TABLE [dbo].[PageSettings]  WITH CHECK ADD  CONSTRAINT [FK_PageSettings_PageSettingsPages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageSettings_PageSettingsPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageSettings]'))
ALTER TABLE [dbo].[PageSettings] CHECK CONSTRAINT [FK_PageSettings_PageSettingsPages]
GO
/****** Object:  ForeignKey [FK_PageWidgets_PageWidgetsPages]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageWidgets_PageWidgetsPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageWidgets]'))
ALTER TABLE [dbo].[PageWidgets]  WITH CHECK ADD  CONSTRAINT [FK_PageWidgets_PageWidgetsPages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageWidgets_PageWidgetsPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageWidgets]'))
ALTER TABLE [dbo].[PageWidgets] CHECK CONSTRAINT [FK_PageWidgets_PageWidgetsPages]
GO
/****** Object:  ForeignKey [FK_PageWidgets_PageWidgetsUsers]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageWidgets_PageWidgetsUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageWidgets]'))
ALTER TABLE [dbo].[PageWidgets]  WITH CHECK ADD  CONSTRAINT [FK_PageWidgets_PageWidgetsUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE SET NULL
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageWidgets_PageWidgetsUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageWidgets]'))
ALTER TABLE [dbo].[PageWidgets] CHECK CONSTRAINT [FK_PageWidgets_PageWidgetsUsers]
GO
/****** Object:  ForeignKey [FK_PageWidgetSettings_PageWidgetSettingsLookAndFeels]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageWidgetSettings_PageWidgetSettingsLookAndFeels]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageWidgetSettings]'))
ALTER TABLE [dbo].[PageWidgetSettings]  WITH CHECK ADD  CONSTRAINT [FK_PageWidgetSettings_PageWidgetSettingsLookAndFeels] FOREIGN KEY([LookAndFeelSettingsId])
REFERENCES [dbo].[LookAndFeelSettings] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageWidgetSettings_PageWidgetSettingsLookAndFeels]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageWidgetSettings]'))
ALTER TABLE [dbo].[PageWidgetSettings] CHECK CONSTRAINT [FK_PageWidgetSettings_PageWidgetSettingsLookAndFeels]
GO
/****** Object:  ForeignKey [FK_PageWidgetSettings_PageWidgetSettingsWidgets]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageWidgetSettings_PageWidgetSettingsWidgets]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageWidgetSettings]'))
ALTER TABLE [dbo].[PageWidgetSettings]  WITH CHECK ADD  CONSTRAINT [FK_PageWidgetSettings_PageWidgetSettingsWidgets] FOREIGN KEY([WidgetId])
REFERENCES [dbo].[PageWidgets] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PageWidgetSettings_PageWidgetSettingsWidgets]') AND parent_object_id = OBJECT_ID(N'[dbo].[PageWidgetSettings]'))
ALTER TABLE [dbo].[PageWidgetSettings] CHECK CONSTRAINT [FK_PageWidgetSettings_PageWidgetSettingsWidgets]
GO
/****** Object:  ForeignKey [FK_Permissions_FK_Permissions_EntityTypes]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_FK_Permissions_EntityTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permissions]'))
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_FK_Permissions_EntityTypes] FOREIGN KEY([EntityTypeId])
REFERENCES [dbo].[EntityTypes] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_FK_Permissions_EntityTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permissions]'))
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_FK_Permissions_EntityTypes]
GO
/****** Object:  ForeignKey [FK_Permissions_FK_Permissions_Users]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_FK_Permissions_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permissions]'))
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_FK_Permissions_Users] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_FK_Permissions_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permissions]'))
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_FK_Permissions_Users]
GO
/****** Object:  ForeignKey [FK_SiteMapWidgets_SiteMapWidgetRootPages]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SiteMapWidgets_SiteMapWidgetRootPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[SiteMapWidgets]'))
ALTER TABLE [dbo].[SiteMapWidgets]  WITH CHECK ADD  CONSTRAINT [FK_SiteMapWidgets_SiteMapWidgetRootPages] FOREIGN KEY([RootPageId])
REFERENCES [dbo].[Pages] ([Id])
ON DELETE SET NULL
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SiteMapWidgets_SiteMapWidgetRootPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[SiteMapWidgets]'))
ALTER TABLE [dbo].[SiteMapWidgets] CHECK CONSTRAINT [FK_SiteMapWidgets_SiteMapWidgetRootPages]
GO
/****** Object:  ForeignKey [FK_UserGroupsMembers_FK_UserGroupsMembers_UserGroups]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserGroupsMembers_FK_UserGroupsMembers_UserGroups]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroupsMembers]'))
ALTER TABLE [dbo].[UserGroupsMembers]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupsMembers_FK_UserGroupsMembers_UserGroups] FOREIGN KEY([UserGroupId])
REFERENCES [dbo].[UserGroups] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserGroupsMembers_FK_UserGroupsMembers_UserGroups]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroupsMembers]'))
ALTER TABLE [dbo].[UserGroupsMembers] CHECK CONSTRAINT [FK_UserGroupsMembers_FK_UserGroupsMembers_UserGroups]
GO
/****** Object:  ForeignKey [FK_UserGroupsMembers_FK_UserGroupsMembers_Users]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserGroupsMembers_FK_UserGroupsMembers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroupsMembers]'))
ALTER TABLE [dbo].[UserGroupsMembers]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupsMembers_FK_UserGroupsMembers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserGroupsMembers_FK_UserGroupsMembers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroupsMembers]'))
ALTER TABLE [dbo].[UserGroupsMembers] CHECK CONSTRAINT [FK_UserGroupsMembers_FK_UserGroupsMembers_Users]
GO
/****** Object:  ForeignKey [FK_UserGroupsToRoles_FK_UserGroupsToRoles_Roles]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserGroupsToRoles_FK_UserGroupsToRoles_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroupsToRoles]'))
ALTER TABLE [dbo].[UserGroupsToRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupsToRoles_FK_UserGroupsToRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserGroupsToRoles_FK_UserGroupsToRoles_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroupsToRoles]'))
ALTER TABLE [dbo].[UserGroupsToRoles] CHECK CONSTRAINT [FK_UserGroupsToRoles_FK_UserGroupsToRoles_Roles]
GO
/****** Object:  ForeignKey [FK_UserGroupsToRoles_FK_UserGroupsToRoles_UserGroups]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserGroupsToRoles_FK_UserGroupsToRoles_UserGroups]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroupsToRoles]'))
ALTER TABLE [dbo].[UserGroupsToRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupsToRoles_FK_UserGroupsToRoles_UserGroups] FOREIGN KEY([UserGroupId])
REFERENCES [dbo].[UserGroups] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserGroupsToRoles_FK_UserGroupsToRoles_UserGroups]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserGroupsToRoles]'))
ALTER TABLE [dbo].[UserGroupsToRoles] CHECK CONSTRAINT [FK_UserGroupsToRoles_FK_UserGroupsToRoles_UserGroups]
GO
/****** Object:  ForeignKey [FK_UsersToRoles_FK_UsersToRoles_Roles]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersToRoles_FK_UsersToRoles_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersToRoles]'))
ALTER TABLE [dbo].[UsersToRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersToRoles_FK_UsersToRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersToRoles_FK_UsersToRoles_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersToRoles]'))
ALTER TABLE [dbo].[UsersToRoles] CHECK CONSTRAINT [FK_UsersToRoles_FK_UsersToRoles_Roles]
GO
/****** Object:  ForeignKey [FK_UsersToRoles_FK_UsersToRoles_Users]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersToRoles_FK_UsersToRoles_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersToRoles]'))
ALTER TABLE [dbo].[UsersToRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersToRoles_FK_UsersToRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersToRoles_FK_UsersToRoles_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersToRoles]'))
ALTER TABLE [dbo].[UsersToRoles] CHECK CONSTRAINT [FK_UsersToRoles_FK_UsersToRoles_Users]
GO
/****** Object:  ForeignKey [FK_Widgets_WidgetPlugins]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Widgets_WidgetPlugins]') AND parent_object_id = OBJECT_ID(N'[dbo].[Widgets]'))
ALTER TABLE [dbo].[Widgets]  WITH CHECK ADD  CONSTRAINT [FK_Widgets_WidgetPlugins] FOREIGN KEY([PluginId])
REFERENCES [dbo].[Plugins] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Widgets_WidgetPlugins]') AND parent_object_id = OBJECT_ID(N'[dbo].[Widgets]'))
ALTER TABLE [dbo].[Widgets] CHECK CONSTRAINT [FK_Widgets_WidgetPlugins]
GO
/****** Object:  ForeignKey [FK_ContentPageWidgets_ContentPages]    Script Date: 06/09/2011 16:29:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContentPageWidgets_ContentPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContentPageWidgets]'))
ALTER TABLE [dbo].[ContentPageWidgets]  WITH CHECK ADD  CONSTRAINT [FK_ContentPageWidgets_ContentPages] FOREIGN KEY([ContentPageId])
REFERENCES [dbo].[ContentPages] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContentPageWidgets_ContentPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContentPageWidgets]'))
ALTER TABLE [dbo].[ContentPageWidgets] CHECK CONSTRAINT [FK_ContentPageWidgets_ContentPages]
GO
