SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroups](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteSettings]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteSettings](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ShowPanel] [bit] NOT NULL,
	[WebsiteName] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SiteSettings] ON
INSERT [dbo].[SiteSettings] ([Id], [ShowPanel], [WebsiteName]) VALUES (1, 1, N'Core Framework')
SET IDENTITY_INSERT [dbo].[SiteSettings] OFF
/****** Object:  Table [dbo].[WebContent_Sections]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebContent_Sections](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UserId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebContent_DetailsWidgets]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebContent_DetailsWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[LinkMode] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[WebContent_DetailsWidgets] ON
INSERT [dbo].[WebContent_DetailsWidgets] ([Id], [LinkMode]) VALUES (1, 1)
SET IDENTITY_INSERT [dbo].[WebContent_DetailsWidgets] OFF
/****** Object:  Table [dbo].[LookAndFeelSettings]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LookAndFeelSettings](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BackgroundColor] [nvarchar](7) NULL,
	[FontFamily] [nvarchar](50) NULL,
	[FontSizeValue] [float] NULL,
	[FontSizeUnit] [nvarchar](50) NULL,
	[Color] [nvarchar](7) NULL,
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
GO
SET IDENTITY_INSERT [dbo].[LookAndFeelSettings] ON
INSERT [dbo].[LookAndFeelSettings] ([Id], [BackgroundColor], [FontFamily], [FontSizeValue], [FontSizeUnit], [Color], [WidthValue], [WidthUnit], [HeightValue], [HeightUnit], [OtherStyles]) VALUES (54, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'border: 1px solid #DFE5E8; padding:10px;')
INSERT [dbo].[LookAndFeelSettings] ([Id], [BackgroundColor], [FontFamily], [FontSizeValue], [FontSizeUnit], [Color], [WidthValue], [WidthUnit], [HeightValue], [HeightUnit], [OtherStyles]) VALUES (55, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[LookAndFeelSettings] ([Id], [BackgroundColor], [FontFamily], [FontSizeValue], [FontSizeUnit], [Color], [WidthValue], [WidthUnit], [HeightValue], [HeightUnit], [OtherStyles]) VALUES (58, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[LookAndFeelSettings] ([Id], [BackgroundColor], [FontFamily], [FontSizeValue], [FontSizeUnit], [Color], [WidthValue], [WidthUnit], [HeightValue], [HeightUnit], [OtherStyles]) VALUES (59, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[LookAndFeelSettings] ([Id], [BackgroundColor], [FontFamily], [FontSizeValue], [FontSizeUnit], [Color], [WidthValue], [WidthUnit], [HeightValue], [HeightUnit], [OtherStyles]) VALUES (61, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[LookAndFeelSettings] OFF
/****** Object:  Table [dbo].[ListMenuWidgets]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListMenuWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Orientation] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NavigationMenuWidgets]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NavigationMenuWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Orientation] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormLogin_FormLoginWidgets]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormLogin_FormLoginWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ShowTitle] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[FormLogin_FormLoginWidgets] ON
INSERT [dbo].[FormLogin_FormLoginWidgets] ([Id], [ShowTitle]) VALUES (8, 0)
INSERT [dbo].[FormLogin_FormLoginWidgets] ([Id], [ShowTitle]) VALUES (9, 1)
INSERT [dbo].[FormLogin_FormLoginWidgets] ([Id], [ShowTitle]) VALUES (10, 1)
INSERT [dbo].[FormLogin_FormLoginWidgets] ([Id], [ShowTitle]) VALUES (11, 0)
INSERT [dbo].[FormLogin_FormLoginWidgets] ([Id], [ShowTitle]) VALUES (12, 0)
SET IDENTITY_INSERT [dbo].[FormLogin_FormLoginWidgets] OFF
/****** Object:  Table [dbo].[EntityTypes]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityTypes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[EntityTypes] ON
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (1, N'Core.Web.Areas.Navigation.Widgets.NSiteMapWidget')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (2, N'Core.Web.Areas.Navigation.Widgets.NNavigationMenuWidget')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (3, N'Core.Web.Areas.Navigation.Widgets.NListMenuWidget')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (4, N'Core.Web.Areas.Navigation.Widgets.NBreadcrumbsWidget')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (5, N'Core.Web.NHibernate.Models.Page')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (9, N'Core.Web.NHibernate.Models.Plugin')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (326, N'Core.Framework.NHibernate.Models.User')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (327, N'Core.Framework.NHibernate.Models.UserGroup')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (328, N'Core.Framework.NHibernate.Models.Role')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (385, N'Core.Forms.NHibernate.Models.Form')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (396, N'Core.WebContent.NHibernate.Models.Section')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (397, N'Core.WebContent.NHibernate.Models.WebContentCategory')
INSERT [dbo].[EntityTypes] ([Id], [Name]) VALUES (398, N'Core.WebContent.NHibernate.Models.Article')
SET IDENTITY_INSERT [dbo].[EntityTypes] OFF
/****** Object:  Table [dbo].[BreadcrumbsWidgets]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BreadcrumbsWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ShowHomePage] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages_Languages]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages_Languages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Code] [nvarchar](3) NOT NULL,
	[Culture] [nvarchar](10) NOT NULL,
	[IsDefault] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageLayoutTemplates]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageLayoutTemplates](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[LayoutCssClass] [nvarchar](255) NOT NULL,
	[Priority] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PageLayoutTemplates] ON
INSERT [dbo].[PageLayoutTemplates] ([Id], [LayoutCssClass], [Priority]) VALUES (1, N'layout-1', 1)
INSERT [dbo].[PageLayoutTemplates] ([Id], [LayoutCssClass], [Priority]) VALUES (2, N'layout-2', 2)
INSERT [dbo].[PageLayoutTemplates] ([Id], [LayoutCssClass], [Priority]) VALUES (3, N'layout-3', 3)
INSERT [dbo].[PageLayoutTemplates] ([Id], [LayoutCssClass], [Priority]) VALUES (4, N'layout-4', 4)
INSERT [dbo].[PageLayoutTemplates] ([Id], [LayoutCssClass], [Priority]) VALUES (5, N'layout-5', 5)
INSERT [dbo].[PageLayoutTemplates] ([Id], [LayoutCssClass], [Priority]) VALUES (6, N'layout-6', 6)
SET IDENTITY_INSERT [dbo].[PageLayoutTemplates] OFF
/****** Object:  Table [dbo].[Plugins]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plugins](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Identifier] [nvarchar](255) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Version] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Plugins] ON
INSERT [dbo].[Plugins] ([Id], [Identifier], [Status], [CreateDate], [Version]) VALUES (29, N'5224B396-44E1-11E0-B8AF-801ADFD72185', 1, CAST(0x00009FF300E3E11C AS DateTime), NULL)
INSERT [dbo].[Plugins] ([Id], [Identifier], [Status], [CreateDate], [Version]) VALUES (30, N'Core.FormLogin', 0, CAST(0x00009FF300E3E11C AS DateTime), NULL)
INSERT [dbo].[Plugins] ([Id], [Identifier], [Status], [CreateDate], [Version]) VALUES (31, N'123', 1, CAST(0x00009FF300E3E11C AS DateTime), NULL)
INSERT [dbo].[Plugins] ([Id], [Identifier], [Status], [CreateDate], [Version]) VALUES (32, N'1234465465465', 1, CAST(0x00009FF300E3E11C AS DateTime), NULL)
INSERT [dbo].[Plugins] ([Id], [Identifier], [Status], [CreateDate], [Version]) VALUES (33, N'Core.LoginWorkflow', 0, CAST(0x00009FF300E3E11C AS DateTime), NULL)
INSERT [dbo].[Plugins] ([Id], [Identifier], [Status], [CreateDate], [Version]) VALUES (34, N'Core.OpenIDLogin', 0, CAST(0x00009FF300E3E11C AS DateTime), NULL)
INSERT [dbo].[Plugins] ([Id], [Identifier], [Status], [CreateDate], [Version]) VALUES (35, N'Core.Profiles', 0, CAST(0x00009FF300E3E11C AS DateTime), NULL)
INSERT [dbo].[Plugins] ([Id], [Identifier], [Status], [CreateDate], [Version]) VALUES (36, N'Core.WebContent', 1, CAST(0x00009FF300E3E11C AS DateTime), NULL)
INSERT [dbo].[Plugins] ([Id], [Identifier], [Status], [CreateDate], [Version]) VALUES (37, N'calendar', 0, CAST(0x00009FF600FCB688 AS DateTime), NULL)
INSERT [dbo].[Plugins] ([Id], [Identifier], [Status], [CreateDate], [Version]) VALUES (38, N'carousel', 0, CAST(0x00009FF700C8C8C8 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Plugins] OFF
/****** Object:  Table [dbo].[SchemaInfo]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchemaInfo](
	[Version] [bigint] NOT NULL,
	[Key] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_SchemaTmp] PRIMARY KEY CLUSTERED 
(
	[Version] ASC,
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IsSystemRole] [bit] NOT NULL,
	[NotAssignableRole] [bit] NOT NULL,
	[NotPermissible] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT [dbo].[Roles] ([Id], [IsSystemRole], [NotAssignableRole], [NotPermissible]) VALUES (1, 1, 0, 1)
INSERT [dbo].[Roles] ([Id], [IsSystemRole], [NotAssignableRole], [NotPermissible]) VALUES (2, 1, 1, 0)
INSERT [dbo].[Roles] ([Id], [IsSystemRole], [NotAssignableRole], [NotPermissible]) VALUES (3, 1, 1, 1)
INSERT [dbo].[Roles] ([Id], [IsSystemRole], [NotAssignableRole], [NotPermissible]) VALUES (4, 1, 1, 0)
SET IDENTITY_INSERT [dbo].[Roles] OFF
/****** Object:  Table [dbo].[Users]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([Id], [Username], [Email], [Hash], [Salt], [EncryptionMode], [Status]) VALUES (1, N'admin', N'admin@admin.com', N'sHk9iZUEf//2l3O1Km0OJmZYFaF+UXESQ9UXH1odvDM=', N'HwYOn0O1nexScBazpgXsS8oX95oYosfGUhV1hTF17XQ=', 3, 0)
INSERT [dbo].[Users] ([Id], [Username], [Email], [Hash], [Salt], [EncryptionMode], [Status]) VALUES (10, N'alexandra.ip.s@gmail.com', N'alexandra.ip.s@gmail.com', N'UN5w+7+KdsVMg2myGAHxcaSO1wO5Z2Kh2MyWZET+zFQ=', N'HPQv9JXMwki6SQ4ogbMLqnBU21cpuUuDbzIxw9qfCyE=', 3, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Table [dbo].[UserGroupsToRoles]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroupsToRoles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserGroupId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroupsMembers]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroupsMembers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[UserGroupId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebContent_Categories]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebContent_Categories](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UserId] [bigint] NULL,
	[Status] [int] NOT NULL,
	[SectionId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleLocales]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleLocales](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Culture] [nvarchar](10) NULL,
	[RoleId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RoleLocales] ON
INSERT [dbo].[RoleLocales] ([Id], [Name], [Culture], [RoleId]) VALUES (1, N'Administrator', NULL, 1)
INSERT [dbo].[RoleLocales] ([Id], [Name], [Culture], [RoleId]) VALUES (2, N'Guest', NULL, 2)
INSERT [dbo].[RoleLocales] ([Id], [Name], [Culture], [RoleId]) VALUES (3, N'Owner', NULL, 3)
INSERT [dbo].[RoleLocales] ([Id], [Name], [Culture], [RoleId]) VALUES (4, N'User', NULL, 4)
SET IDENTITY_INSERT [dbo].[RoleLocales] OFF
/****** Object:  Table [dbo].[PluginLocales]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PluginLocales](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Culture] [nvarchar](10) NULL,
	[PluginId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PluginLocales] ON
INSERT [dbo].[PluginLocales] ([Id], [Title], [Description], [Culture], [PluginId]) VALUES (21, N'Navigation', N'Navigation', NULL, 29)
INSERT [dbo].[PluginLocales] ([Id], [Title], [Description], [Culture], [PluginId]) VALUES (22, N'Form login', N'
    Allow user sign-in using login form.
  ', NULL, 30)
INSERT [dbo].[PluginLocales] ([Id], [Title], [Description], [Culture], [PluginId]) VALUES (23, N'Forms', N'
    Allow managing Web Forms.
  ', NULL, 31)
INSERT [dbo].[PluginLocales] ([Id], [Title], [Description], [Culture], [PluginId]) VALUES (24, N'Languages', N'
    Allow managing Languages.
  ', NULL, 32)
INSERT [dbo].[PluginLocales] ([Id], [Title], [Description], [Culture], [PluginId]) VALUES (25, N'Login workflow', N'
    Allow user sign-in using forms login or OpenID.
  ', NULL, 33)
INSERT [dbo].[PluginLocales] ([Id], [Title], [Description], [Culture], [PluginId]) VALUES (26, N'OpenID Login', N'
    Allow user sign-in using OpenID.
  ', NULL, 34)
INSERT [dbo].[PluginLocales] ([Id], [Title], [Description], [Culture], [PluginId]) VALUES (27, N'Profiles', N'
    Allow managing profiles.
  ', NULL, 35)
INSERT [dbo].[PluginLocales] ([Id], [Title], [Description], [Culture], [PluginId]) VALUES (28, N'Web Content', N'
    Allow managing web content.
  ', NULL, 36)
INSERT [dbo].[PluginLocales] ([Id], [Title], [Description], [Culture], [PluginId]) VALUES (29, N'Calendar', N'Allow managing calendars.', NULL, 37)
INSERT [dbo].[PluginLocales] ([Id], [Title], [Description], [Culture], [PluginId]) VALUES (30, N'Carousel', N'Allow managing carousels.', NULL, 38)
SET IDENTITY_INSERT [dbo].[PluginLocales] OFF
/****** Object:  Table [dbo].[Permissions]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1, 1, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2, 1, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (3, 1, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (4, 2, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (5, 2, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (6, 2, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (7, 1, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (8, 1, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (9, 1, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (10, 2, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (11, 2, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (12, 2, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (13, 3, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (14, 3, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (15, 3, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (19, 3, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (20, 3, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (21, 3, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (22, 4, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (23, 4, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (24, 4, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (25, 5, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (26, 5, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (27, 5, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (28, 6, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (29, 6, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (30, 6, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (31, 7, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (32, 7, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (33, 7, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (34, 5, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (35, 5, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (36, 5, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (37, 6, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (38, 6, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (39, 6, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (40, 7, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (41, 7, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (42, 7, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (109, 8, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (110, 8, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (111, 8, 5, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (133, 9, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (134, 9, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (135, 9, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (139, 10, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (140, 10, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (141, 10, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (145, 34, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (146, 34, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (147, 34, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (148, 11, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (149, 11, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (150, 11, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (151, 38, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (152, 38, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (153, 38, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (154, 39, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (155, 39, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (156, 39, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (157, 12, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (158, 12, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (159, 12, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (166, 8, 0, 5, 1)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (172, 13, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (173, 13, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (174, 13, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (175, 41, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (176, 41, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (177, 41, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (178, 42, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (179, 42, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (180, 42, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (181, 43, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (182, 43, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (183, 43, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (184, 44, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (185, 44, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (186, 44, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (193, 47, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (194, 47, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (195, 47, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (205, 14, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (206, 14, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (207, 14, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (289, 15, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (290, 15, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (291, 15, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (295, 78, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (296, 78, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (297, 78, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (298, 16, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (299, 16, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (300, 16, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (301, 80, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (302, 80, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (303, 80, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (307, 83, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (308, 83, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (309, 83, 1, 2, 2)
GO
print 'Processed 100 total records'
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (310, 84, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (311, 84, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (312, 84, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (313, 85, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (314, 85, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (315, 85, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (316, 86, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (317, 86, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (318, 86, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (319, 87, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (320, 87, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (321, 87, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (322, 88, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (323, 88, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (324, 88, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (343, 17, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (344, 17, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (345, 17, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (346, 95, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (347, 95, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (348, 95, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (349, 18, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (350, 18, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (351, 18, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (352, 20, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (353, 19, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (354, 20, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (355, 19, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (356, 20, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (357, 19, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (358, 21, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (359, 21, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (360, 21, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (361, 22, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (362, 22, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (363, 22, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (370, 100, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (371, 100, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (372, 100, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (373, 23, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (374, 23, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (375, 23, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (376, 24, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (377, 24, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (378, 24, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (385, 106, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (386, 106, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (387, 106, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (388, 25, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (389, 25, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (390, 25, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (391, 107, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (392, 107, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (393, 107, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (400, 26, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (401, 26, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (402, 26, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (403, 27, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (404, 27, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (405, 27, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (406, 28, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (407, 28, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (408, 28, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (409, 29, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (410, 29, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (411, 29, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (412, 30, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (413, 30, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (414, 30, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (415, 31, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (416, 31, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (417, 31, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (439, 111, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (440, 111, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (441, 111, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (442, 32, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (443, 32, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (444, 32, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (451, NULL, 3, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (453, NULL, 7, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (454, 51, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (455, 51, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (456, 51, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (457, 56, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (458, 56, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (459, 56, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (469, 116, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (470, 116, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (471, 116, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (487, 122, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (488, 122, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (489, 122, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (496, 125, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (497, 125, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (498, 125, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (499, 126, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (500, 126, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (501, 126, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (508, 57, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (509, 57, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (510, 57, 1, 5, 2)
GO
print 'Processed 200 total records'
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (514, 58, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (515, 58, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (516, 58, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (529, 63, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (530, 63, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (531, 63, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (535, 66, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (536, 66, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (537, 66, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (538, 136, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (539, 136, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (540, 136, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (580, 69, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (581, 69, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (582, 69, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (592, 70, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (593, 70, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (594, 70, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (595, 71, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (596, 71, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (597, 71, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (617, 146, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (618, 146, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (619, 146, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (620, 147, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (621, 147, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (622, 147, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (623, 148, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (624, 148, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (625, 148, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (629, 72, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (630, 72, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (631, 72, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (632, 73, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (633, 73, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (634, 73, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (641, 153, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (642, 153, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (643, 153, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (644, 74, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (645, 74, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (646, 74, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (647, 75, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (648, 75, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (649, 75, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (650, 76, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (651, 76, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (652, 76, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (668, 162, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (669, 162, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (670, 162, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (671, 78, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (672, 78, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (673, 78, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (674, 79, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (675, 79, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (676, 79, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (677, 165, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (678, 165, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (679, 165, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (731, 170, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (732, 170, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (733, 170, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (740, 80, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (741, 80, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (742, 80, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (788, 182, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (789, 182, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (790, 182, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (791, 183, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (792, 183, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (793, 183, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (794, 184, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (795, 184, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (796, 184, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (797, 81, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (798, 81, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (799, 81, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (809, 82, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (810, 82, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (811, 82, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (815, 191, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (816, 191, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (817, 191, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (818, 192, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (819, 192, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (820, 192, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (821, 193, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (822, 193, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (823, 193, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (824, 194, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (825, 194, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (826, 194, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (827, 83, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (828, 83, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (829, 83, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (830, 84, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (831, 84, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (832, 84, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (833, 197, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (834, 197, 1, 4, 4)
GO
print 'Processed 300 total records'
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (835, 197, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (839, 199, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (840, 199, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (841, 199, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (842, 200, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (843, 200, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (844, 200, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (845, 86, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (846, 86, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (847, 86, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (857, 204, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (858, 204, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (859, 204, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (860, 205, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (861, 205, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (862, 205, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (863, 206, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (864, 206, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (865, 206, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (872, 87, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (873, 87, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (874, 87, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (875, 88, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (876, 88, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (877, 88, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (878, 89, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (879, 89, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (880, 89, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (881, 90, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (882, 90, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (883, 90, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (884, 91, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (885, 91, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (886, 91, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (887, 92, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (888, 92, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (889, 92, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (890, 93, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (891, 93, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (892, 93, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (893, 94, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (894, 94, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (895, 94, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (896, 95, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (897, 95, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (898, 95, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (899, 96, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (900, 96, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (901, 96, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (905, 243, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (906, 243, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (907, 243, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (908, 244, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (909, 244, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (910, 244, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (914, 99, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (915, 99, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (916, 99, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (923, 100, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (924, 100, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (925, 100, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (929, 101, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (930, 101, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (931, 101, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (935, 254, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (936, 254, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (937, 254, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (938, 104, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (939, 104, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (940, 104, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (944, 105, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (945, 105, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (946, 105, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (947, 106, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (948, 106, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (949, 106, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (950, 107, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (951, 107, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (952, 107, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (956, 264, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (957, 264, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (958, 264, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (962, 266, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (963, 266, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (964, 266, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (965, 267, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (966, 267, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (967, 267, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (977, 108, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (978, 108, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (979, 108, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (986, 109, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (987, 109, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (988, 109, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (989, 110, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (990, 110, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (991, 110, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (992, 111, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (993, 111, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (994, 111, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (995, 112, 15, 5, 3)
GO
print 'Processed 400 total records'
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (996, 112, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (997, 112, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (998, 113, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (999, 113, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1000, 113, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1001, 114, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1002, 114, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1003, 114, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1004, 115, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1005, 115, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1006, 115, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1007, 116, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1008, 116, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1009, 116, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1010, 117, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1011, 117, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1012, 117, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1013, 118, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1014, 118, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1015, 118, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1016, 119, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1017, 119, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1018, 119, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1019, 120, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1020, 120, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1021, 120, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1022, 121, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1023, 121, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1024, 121, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1025, 122, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1026, 122, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1027, 122, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1028, 123, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1029, 123, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1030, 123, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1031, 124, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1032, 124, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1033, 124, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1034, 274, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1035, 274, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1036, 274, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1037, 275, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1038, 275, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1039, 275, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1043, 125, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1044, 125, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1045, 125, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1046, 126, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1047, 126, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1048, 126, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1055, 128, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1056, 128, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1057, 128, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1064, 131, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1065, 131, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1066, 131, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1082, 133, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1083, 133, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1084, 133, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1085, 293, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1086, 293, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1087, 293, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1088, 294, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1089, 294, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1090, 294, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1091, 134, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1092, 134, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1093, 134, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1094, 135, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1095, 135, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1096, 135, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1097, 297, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1098, 297, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1099, 297, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1100, 136, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1101, 136, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1102, 136, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1103, 137, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1104, 137, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1105, 137, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1106, 138, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1107, 138, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1108, 138, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1109, 139, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1110, 139, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1111, 139, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1112, 301, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1113, 301, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1114, 301, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1115, 303, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1116, 303, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1117, 303, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1118, 141, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1119, 141, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1120, 141, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1121, 305, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1122, 305, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1123, 305, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1130, 143, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1131, 143, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1132, 143, 1, 5, 2)
GO
print 'Processed 500 total records'
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1133, 144, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1134, 144, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1135, 144, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1136, 145, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1137, 145, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1138, 145, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1145, 146, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1146, 146, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1147, 146, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1148, 147, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1149, 147, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1150, 147, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1151, 148, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1152, 148, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1153, 148, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1154, 149, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1155, 149, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1156, 149, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1157, 150, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1158, 150, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1159, 150, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1160, 151, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1161, 151, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1162, 151, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1163, 152, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1164, 152, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1165, 152, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1169, 153, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1170, 153, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1171, 153, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1172, 154, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1173, 154, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1174, 154, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1196, 164, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1197, 164, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1198, 164, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1199, 165, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1200, 165, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1201, 165, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1202, 166, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1203, 166, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1204, 166, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1205, 167, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1206, 167, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1207, 167, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1208, 168, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1209, 168, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1210, 168, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1211, 169, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1212, 169, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1213, 169, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1214, 170, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1215, 170, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1216, 170, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1217, 171, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1218, 171, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1219, 171, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1220, 172, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1221, 172, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1222, 172, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1223, 174, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1224, 174, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1225, 174, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1238, 177, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1239, 177, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1240, 177, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1241, 179, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1242, 179, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1243, 179, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1244, 180, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1245, 180, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1246, 180, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1247, 181, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1248, 181, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1249, 181, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1250, 182, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1251, 182, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1252, 182, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1253, 183, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1254, 183, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1255, 183, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1256, 184, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1257, 184, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1258, 184, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1259, 185, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1260, 185, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1261, 185, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1262, 187, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1263, 187, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1264, 187, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1268, 189, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1269, 189, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1270, 189, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1271, 190, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1272, 190, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1273, 190, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1274, 401, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1275, 401, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1276, 401, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1277, 402, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1278, 402, 1, 1, 4)
GO
print 'Processed 600 total records'
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1279, 402, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1280, 403, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1281, 403, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1282, 403, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1286, 191, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1287, 191, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1288, 191, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1289, 192, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1290, 192, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1291, 192, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1292, 193, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1293, 193, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1294, 193, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1295, 410, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1296, 410, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1297, 410, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1298, 411, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1299, 411, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1300, 411, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1301, 412, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1302, 412, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1303, 412, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1304, 413, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1305, 413, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1306, 413, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1307, 414, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1308, 414, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1309, 414, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1310, 415, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1311, 415, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1312, 415, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1313, 419, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1314, 419, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1315, 419, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1316, 420, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1317, 420, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1318, 420, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1319, 421, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1320, 421, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1321, 421, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1325, 195, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1326, 195, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1327, 195, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1328, 196, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1329, 196, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1330, 196, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1331, 197, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1332, 197, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1333, 197, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1334, 426, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1335, 426, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1336, 426, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1337, 427, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1338, 427, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1339, 427, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1340, 428, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1341, 428, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1342, 428, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1346, 199, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1347, 199, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1348, 199, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1349, 434, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1350, 434, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1351, 434, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1352, 435, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1353, 435, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1354, 435, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1355, 436, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1356, 436, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1357, 436, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1361, 200, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1362, 200, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1363, 200, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1364, 441, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1365, 441, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1366, 441, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1367, 442, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1368, 442, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1369, 442, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1370, 443, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1371, 443, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1372, 443, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1376, 445, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1377, 445, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1378, 445, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1379, 446, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1380, 446, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1381, 446, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1382, 447, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1383, 447, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1384, 447, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1388, 201, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1389, 201, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1390, 201, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1391, 202, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1392, 202, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1393, 202, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1394, 203, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1395, 203, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1396, 203, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1397, 452, 15, 1, 3)
GO
print 'Processed 700 total records'
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1398, 452, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1399, 452, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1400, 453, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1401, 453, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1402, 453, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1403, 454, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1404, 454, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1405, 454, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1409, 456, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1410, 456, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1411, 456, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1412, 457, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1413, 457, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1414, 457, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1415, 458, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1416, 458, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1417, 458, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1421, 460, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1422, 460, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1423, 460, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1424, 461, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1425, 461, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1426, 461, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1427, 462, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1428, 462, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1429, 462, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1433, 430, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1434, 430, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1435, 430, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1436, 431, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1437, 431, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1438, 431, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1439, 432, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1440, 432, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1441, 432, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1445, 464, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1446, 464, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1447, 464, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1448, 465, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1449, 465, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1450, 465, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1451, 466, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1452, 466, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1453, 466, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1460, 469, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1461, 469, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1462, 469, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1463, 204, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1464, 204, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1465, 204, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1466, 205, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1467, 205, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1468, 205, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1472, 207, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1473, 207, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1474, 207, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1475, 208, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1476, 208, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1477, 208, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1478, 209, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1479, 209, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1480, 209, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1481, 210, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1482, 210, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1483, 210, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1484, 211, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1485, 211, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1486, 211, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1487, 486, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1488, 486, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1489, 486, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1490, 487, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1491, 487, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1492, 487, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1493, 213, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1494, 213, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1495, 213, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1496, 214, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1497, 214, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1498, 214, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1499, 215, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1500, 215, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1501, 215, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1502, 217, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1503, 217, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1504, 217, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1505, 218, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1506, 218, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1507, 218, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1508, 219, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1509, 219, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1510, 219, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1511, 495, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1512, 495, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1513, 495, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1514, 496, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1515, 496, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1516, 496, 1, 2, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1517, 498, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1518, 498, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1519, 498, 1, 1, 2)
GO
print 'Processed 800 total records'
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1520, 499, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1521, 499, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1522, 499, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1523, 222, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1524, 222, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1525, 222, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1526, 223, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1527, 223, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1528, 223, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1529, 225, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1530, 225, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1531, 225, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1532, 226, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1533, 226, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1534, 226, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1535, 228, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1536, 228, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1537, 228, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1538, 230, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1539, 230, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1540, 230, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1541, 509, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1542, 509, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1543, 509, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1544, 510, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1545, 510, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1546, 510, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1547, 515, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1548, 515, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1549, 515, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1550, 232, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1551, 232, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1552, 232, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1553, 516, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1554, 516, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1555, 516, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1556, 517, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1557, 517, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1558, 517, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1559, 518, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1560, 518, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1561, 518, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1562, 519, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1563, 519, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1564, 519, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1565, 234, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1566, 234, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1567, 234, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1568, 520, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1569, 520, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1570, 520, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1571, 521, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1572, 521, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1573, 521, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1574, 522, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1575, 522, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1576, 522, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1577, 523, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1578, 523, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1579, 523, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1580, 235, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1581, 235, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1582, 235, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1583, 236, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1584, 236, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1585, 236, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1586, 526, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1587, 526, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1588, 526, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1589, 527, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1590, 527, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1591, 527, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1592, 237, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1593, 237, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1594, 237, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1595, 528, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1596, 528, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1597, 528, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1598, 529, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1599, 529, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1600, 529, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1601, 530, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1602, 530, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1603, 530, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1604, 531, 15, 1, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1605, 531, 1, 1, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1606, 531, 1, 1, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1607, 532, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1608, 532, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1609, 532, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1610, 533, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1611, 533, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1612, 533, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1613, 238, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1614, 238, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1615, 238, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1616, 534, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1617, 534, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1618, 534, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1619, 535, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1620, 535, 1, 3, 4)
GO
print 'Processed 900 total records'
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1621, 535, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1622, 536, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1623, 536, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1624, 536, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1625, 537, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1626, 537, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1627, 537, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1628, 239, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1629, 239, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1630, 239, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1631, 538, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1632, 538, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1633, 538, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1634, 539, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1635, 539, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1636, 539, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1637, 540, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1638, 540, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1639, 540, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1640, 541, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1641, 541, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1642, 541, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1643, 240, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1644, 240, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1645, 240, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1646, 542, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1647, 542, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1648, 542, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1649, 543, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1650, 543, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1651, 543, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1652, 241, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1653, 241, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1654, 241, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1655, 544, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1656, 544, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1657, 544, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1658, 545, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1659, 545, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1660, 545, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1661, 546, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1662, 546, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1663, 546, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1664, 547, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1665, 547, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1666, 547, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1667, 242, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1668, 242, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1669, 242, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1670, 548, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1671, 548, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1672, 548, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1673, 549, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1674, 549, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1675, 549, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1676, 550, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1677, 550, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1678, 550, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1679, 551, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1680, 551, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1681, 551, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1682, 552, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1683, 552, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1684, 552, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1685, 553, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1686, 553, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1687, 553, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1688, 243, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1689, 243, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1690, 243, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1691, 554, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1692, 554, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1693, 554, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1694, 555, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1695, 555, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1696, 555, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1697, 556, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1698, 556, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1699, 556, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1700, 557, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1701, 557, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1702, 557, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1703, 244, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1704, 244, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1705, 244, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1706, 558, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1707, 558, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1708, 558, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1709, 559, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1710, 559, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1711, 559, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1712, 245, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1713, 245, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1714, 245, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1715, 560, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1716, 560, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1717, 560, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1718, 561, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1719, 561, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1720, 561, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1721, 562, 15, 3, 3)
GO
print 'Processed 1000 total records'
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1722, 562, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1723, 562, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1724, 563, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1725, 563, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1726, 563, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1727, 246, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1728, 246, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1729, 246, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1730, 247, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1731, 247, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1732, 247, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1733, 248, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1734, 248, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1735, 248, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1736, 249, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1737, 249, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1738, 249, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1739, 572, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1740, 572, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1741, 572, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1742, 573, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1743, 573, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1744, 573, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1745, 574, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1746, 574, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1747, 574, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1748, 575, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1749, 575, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1750, 575, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1751, 250, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1752, 250, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1753, 250, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1754, 576, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1755, 576, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1756, 576, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1757, 577, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1758, 577, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1759, 577, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1760, 578, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1761, 578, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1762, 578, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1763, 579, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1764, 579, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1765, 579, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1766, 251, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1767, 251, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1768, 251, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1769, 580, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1770, 580, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1771, 580, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1772, 581, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1773, 581, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1774, 581, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1775, 253, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1776, 253, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1777, 253, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1778, 254, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1779, 254, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1780, 254, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1781, 255, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1782, 255, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1783, 255, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1784, 584, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1785, 584, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1786, 584, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1787, 586, 15, 3, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1788, 586, 1, 3, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1789, 586, 1, 3, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1790, 258, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1791, 258, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1792, 258, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1820, 589, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1821, 589, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1822, 589, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1823, 590, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1824, 590, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1825, 590, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1829, 592, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1830, 592, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1831, 592, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1832, 593, 15, 4, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1833, 593, 1, 4, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1834, 593, 1, 4, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1874, 259, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1875, 259, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1876, 259, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1886, 260, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1887, 260, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1888, 260, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1902, 261, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1903, 261, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1904, 261, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1927, 263, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1928, 263, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (1929, 263, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2032, 265, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2033, 265, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2034, 265, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2035, 266, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2036, 266, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2037, 266, 1, 5, 2)
GO
print 'Processed 1100 total records'
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2038, 267, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2039, 267, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2040, 267, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2041, 268, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2042, 268, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2043, 268, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2044, 269, 15, 5, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2045, 269, 1, 5, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2046, 269, 1, 5, 2)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2050, 651, 15, 2, 3)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2051, 651, 1, 2, 4)
INSERT [dbo].[Permissions] ([Id], [EntityId], [Permissions], [EntityTypeId], [RoleId]) VALUES (2052, 651, 1, 2, 2)
SET IDENTITY_INSERT [dbo].[Permissions] OFF
/****** Object:  Table [dbo].[PageLayoutRows]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageLayoutRows](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TemplateId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PageLayoutRows] ON
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (1, 1)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (2, 2)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (3, 3)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (4, 3)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (5, 4)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (6, 4)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (7, 5)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (8, 5)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (9, 5)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (10, 6)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (11, 6)
INSERT [dbo].[PageLayoutRows] ([Id], [TemplateId]) VALUES (12, 6)
SET IDENTITY_INSERT [dbo].[PageLayoutRows] OFF
/****** Object:  Table [dbo].[Forms_Forms]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forms_Forms](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ShowSubmitButton] [bit] NOT NULL,
	[ShowResetButton] [bit] NOT NULL,
	[UserId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Forms_Forms] ON
INSERT [dbo].[Forms_Forms] ([Id], [ShowSubmitButton], [ShowResetButton], [UserId]) VALUES (1, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Forms_Forms] OFF
/****** Object:  Table [dbo].[Migrations]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Migrations](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [bigint] NOT NULL,
	[PluginId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](255) NOT NULL,
	[ParentPageId] [bigint] NULL,
	[OrderNumber] [int] NULL,
	[UserId] [bigint] NULL,
	[IsServicePage] [bit] NOT NULL,
	[HideInMainMenu] [bit] NOT NULL,
	[IsTemplate] [bit] NOT NULL,
	[TemplateId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Pages] ON
INSERT [dbo].[Pages] ([Id], [Url], [ParentPageId], [OrderNumber], [UserId], [IsServicePage], [HideInMainMenu], [IsTemplate], [TemplateId]) VALUES (254, N'contact-us', NULL, 3, 1, 0, 0, 0, NULL)
INSERT [dbo].[Pages] ([Id], [Url], [ParentPageId], [OrderNumber], [UserId], [IsServicePage], [HideInMainMenu], [IsTemplate], [TemplateId]) VALUES (255, N'widgets', NULL, 2, 1, 0, 0, 0, NULL)
INSERT [dbo].[Pages] ([Id], [Url], [ParentPageId], [OrderNumber], [UserId], [IsServicePage], [HideInMainMenu], [IsTemplate], [TemplateId]) VALUES (256, N'base-template', NULL, 0, 1, 0, 0, 1, NULL)
INSERT [dbo].[Pages] ([Id], [Url], [ParentPageId], [OrderNumber], [UserId], [IsServicePage], [HideInMainMenu], [IsTemplate], [TemplateId]) VALUES (258, N'control-panel', NULL, 4, 1, 0, 0, 0, NULL)
INSERT [dbo].[Pages] ([Id], [Url], [ParentPageId], [OrderNumber], [UserId], [IsServicePage], [HideInMainMenu], [IsTemplate], [TemplateId]) VALUES (260, N'reg-page', NULL, 6, 1, 0, 1, 0, NULL)
INSERT [dbo].[Pages] ([Id], [Url], [ParentPageId], [OrderNumber], [UserId], [IsServicePage], [HideInMainMenu], [IsTemplate], [TemplateId]) VALUES (261, N'home', NULL, 1, 1, 0, 0, 0, NULL)
INSERT [dbo].[Pages] ([Id], [Url], [ParentPageId], [OrderNumber], [UserId], [IsServicePage], [HideInMainMenu], [IsTemplate], [TemplateId]) VALUES (263, N'Login', NULL, 7, 1, 0, 0, 0, NULL)
INSERT [dbo].[Pages] ([Id], [Url], [ParentPageId], [OrderNumber], [UserId], [IsServicePage], [HideInMainMenu], [IsTemplate], [TemplateId]) VALUES (264, N'web-content/details/{webContentId}', NULL, 0, NULL, 1, 0, 0, NULL)
SET IDENTITY_INSERT [dbo].[Pages] OFF
/****** Object:  Table [dbo].[Widgets]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Widgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Identifier] [nvarchar](255) NOT NULL,
	[Status] [int] NOT NULL,
	[PluginId] [bigint] NULL,
	[IsDetailsWidget] [bit] NOT NULL,
	[IsPlaceHolder] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Widgets] ON
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (258, N'4224B396-44E1-11E0-B8AF-801ADFD92185', 0, 29, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (259, N'5224B396-44E1-11E0-B8AF-801ADFD92185', 0, 29, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (260, N'
    4224B2133-4aE1-11E0-B8AF-801Adfg3D92185
  ', 0, NULL, 0, 1)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (261, N'5224B396-44E1-11E0-B8AF-801ADFD921rr5', 0, 29, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (262, N'5224B396-44E1-11E0-B8AF-801ADFD92i85', 0, 29, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (268, N'Core.FormLogin.Widgets.LoginWidget', 0, 30, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (272, N'4224B2133-4aE1-11E0-B8AF-801Adfg3D92185', 0, NULL, 0, 1)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (273, N'23333', 0, 31, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (274, N'46546sadf465', 0, 32, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (275, N'Core.LoginWorkflow.Widgets.LoginHolderWidget', 0, 33, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (276, N'Core.OpenIDLogin.Widgets.OpenIDLoginWidget', 0, 34, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (277, N'Core.Profiles.Widgets.ProfileWidget', 0, 35, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (278, N'Core.Profiles.Widgets.RegistrationWidget', 0, 35, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (279, N'Core.WebContent.Widgets.ContentDetailsWidget', 0, 36, 1, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (280, N'Core.WebContent.Widgets.ContentWidget', 0, 36, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (281, N'calendar123', 0, 37, 0, 0)
INSERT [dbo].[Widgets] ([Id], [Identifier], [Status], [PluginId], [IsDetailsWidget], [IsPlaceHolder]) VALUES (282, N'carousel12345678', 0, 38, 0, 0)
SET IDENTITY_INSERT [dbo].[Widgets] OFF
/****** Object:  Table [dbo].[WebContent_SectionSettings]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebContent_SectionSettings](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[ShowTitle] [int] NOT NULL,
	[TitleLinkable] [int] NOT NULL,
	[ShowSummaryText] [int] NOT NULL,
	[ShowContent] [int] NOT NULL,
	[ShowSection] [int] NOT NULL,
	[ShowCategory] [int] NOT NULL,
	[ShowAuthor] [int] NOT NULL,
	[ShowCreatedDate] [int] NOT NULL,
	[ShowModifiedDate] [int] NOT NULL,
	[ShowDownloadLink] [int] NOT NULL,
	[AlternativeReadMoreText] [nvarchar](255) NULL,
	[SectionId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersToRoles]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersToRoles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UsersToRoles] ON
INSERT [dbo].[UsersToRoles] ([Id], [UserId], [RoleId]) VALUES (17, 1, 1)
SET IDENTITY_INSERT [dbo].[UsersToRoles] OFF
/****** Object:  Table [dbo].[WebContent_SectionLocales]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebContent_SectionLocales](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Culture] [nvarchar](10) NULL,
	[SectionId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteMapWidgets]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  Table [dbo].[WebContent_CategoryLocales]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebContent_CategoryLocales](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Culture] [nvarchar](10) NULL,
	[CategoryId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WidgetLocales]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WidgetLocales](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Culture] [nvarchar](10) NULL,
	[WidgetId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[WidgetLocales] ON
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (191, N'Breadcrumbs', NULL, 258)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (192, N'Site Map', NULL, 259)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (193, N'Place holder', NULL, 260)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (194, N'Navigation Menu', NULL, 261)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (195, N'List Menu', NULL, 262)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (196, N'Login', NULL, 268)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (197, N'Place holder', NULL, 272)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (198, N'Forms Builder', NULL, 273)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (199, N'Language selector', NULL, 274)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (200, N'Login holder', NULL, 275)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (201, N'OpenID Login', NULL, 276)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (202, N'Member profile', NULL, 277)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (203, N'Registration', NULL, 278)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (204, N'Web Content Details', NULL, 279)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (205, N'Web Content', NULL, 280)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (206, N'Calendar', NULL, 281)
INSERT [dbo].[WidgetLocales] ([Id], [Title], [Culture], [WidgetId]) VALUES (207, N'Carousel', NULL, 282)
SET IDENTITY_INSERT [dbo].[WidgetLocales] OFF
/****** Object:  Table [dbo].[PageLocales]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageLocales](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Culture] [nvarchar](10) NULL,
	[PageId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PageLocales] ON
INSERT [dbo].[PageLocales] ([Id], [Title], [Culture], [PageId]) VALUES (240, N'Contact Us', NULL, 254)
INSERT [dbo].[PageLocales] ([Id], [Title], [Culture], [PageId]) VALUES (241, N'Widgets', NULL, 255)
INSERT [dbo].[PageLocales] ([Id], [Title], [Culture], [PageId]) VALUES (242, N'Base template', NULL, 256)
INSERT [dbo].[PageLocales] ([Id], [Title], [Culture], [PageId]) VALUES (244, N'Control Panel', NULL, 258)
INSERT [dbo].[PageLocales] ([Id], [Title], [Culture], [PageId]) VALUES (245, N'Contact Us', N' ', 254)
INSERT [dbo].[PageLocales] ([Id], [Title], [Culture], [PageId]) VALUES (246, N'Control Panel', N' ', 258)
INSERT [dbo].[PageLocales] ([Id], [Title], [Culture], [PageId]) VALUES (248, N'Test registration page', NULL, 260)
INSERT [dbo].[PageLocales] ([Id], [Title], [Culture], [PageId]) VALUES (249, N'Test registration page', N' ', 260)
INSERT [dbo].[PageLocales] ([Id], [Title], [Culture], [PageId]) VALUES (250, N'Home', NULL, 261)
INSERT [dbo].[PageLocales] ([Id], [Title], [Culture], [PageId]) VALUES (252, N'Login', NULL, 263)
INSERT [dbo].[PageLocales] ([Id], [Title], [Culture], [PageId]) VALUES (253, N'Web content details', NULL, 264)
SET IDENTITY_INSERT [dbo].[PageLocales] OFF
/****** Object:  Table [dbo].[PageLayoutColumns]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
SET IDENTITY_INSERT [dbo].[PageLayoutColumns] ON
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (1, 100, NULL, 1)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (2, 25, NULL, 2)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (3, 75, NULL, 2)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (4, 100, 2, 3)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (5, 25, NULL, 4)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (6, 75, NULL, 4)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (7, 75, NULL, 5)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (8, 25, NULL, 5)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (9, 100, 2, 6)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (10, 100, 2, 7)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (11, 25, NULL, 8)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (12, 75, NULL, 8)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (13, 100, 2, 9)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (14, 100, 3, 10)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (15, 33, NULL, 11)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (16, 33, NULL, 11)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (17, 33, NULL, 11)
INSERT [dbo].[PageLayoutColumns] ([Id], [DefaultWidthValue], [DefaultColspan], [RowId]) VALUES (18, 100, 3, 12)
SET IDENTITY_INSERT [dbo].[PageLayoutColumns] OFF
/****** Object:  Table [dbo].[ListMenuWidgetPages]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListMenuWidgetPages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PageId] [bigint] NOT NULL,
	[ListMenuWidgetId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forms_FormLocales]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forms_FormLocales](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[SubmitButtonText] [nvarchar](255) NULL,
	[ResetButtonText] [nvarchar](255) NULL,
	[Culture] [nvarchar](10) NULL,
	[formId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Forms_FormLocales] ON
INSERT [dbo].[Forms_FormLocales] ([Id], [Title], [SubmitButtonText], [ResetButtonText], [Culture], [formId]) VALUES (1, N'Contact Us', N'Submit', N'Reset', NULL, 1)
SET IDENTITY_INSERT [dbo].[Forms_FormLocales] OFF
/****** Object:  Table [dbo].[Forms_FormElements]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forms_FormElements](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[IsRequired] [bit] NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[RegexTemplate] [int] NOT NULL,
	[MaxLength] [bigint] NULL,
	[FormId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Forms_FormElements] ON
INSERT [dbo].[Forms_FormElements] ([Id], [Type], [IsRequired], [OrderNumber], [RegexTemplate], [MaxLength], [FormId]) VALUES (1, 0, 1, 1, 0, 255, 1)
INSERT [dbo].[Forms_FormElements] ([Id], [Type], [IsRequired], [OrderNumber], [RegexTemplate], [MaxLength], [FormId]) VALUES (2, 1, 1, 2, 0, 10000, 1)
INSERT [dbo].[Forms_FormElements] ([Id], [Type], [IsRequired], [OrderNumber], [RegexTemplate], [MaxLength], [FormId]) VALUES (3, 6, 0, 3, 0, NULL, 1)
SET IDENTITY_INSERT [dbo].[Forms_FormElements] OFF
/****** Object:  Table [dbo].[Forms_FormsBuilderWidgets]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forms_FormsBuilderWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[SaveData] [bit] NOT NULL,
	[SendEmail] [bit] NOT NULL,
	[RecipientEmail] [nvarchar](255) NULL,
	[FormId] [bigint] NOT NULL,
	[UserId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Forms_FormsBuilderWidgets] ON
INSERT [dbo].[Forms_FormsBuilderWidgets] ([Id], [Title], [SaveData], [SendEmail], [RecipientEmail], [FormId], [UserId]) VALUES (1, N'Contact Us', 1, 0, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Forms_FormsBuilderWidgets] OFF
/****** Object:  Table [dbo].[PageLayouts]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageLayouts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TemplateId] [bigint] NOT NULL,
	[PageId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PageLayouts] ON
INSERT [dbo].[PageLayouts] ([Id], [TemplateId], [PageId]) VALUES (209, 1, 254)
INSERT [dbo].[PageLayouts] ([Id], [TemplateId], [PageId]) VALUES (210, 3, 255)
INSERT [dbo].[PageLayouts] ([Id], [TemplateId], [PageId]) VALUES (211, 1, 256)
INSERT [dbo].[PageLayouts] ([Id], [TemplateId], [PageId]) VALUES (213, 1, 258)
INSERT [dbo].[PageLayouts] ([Id], [TemplateId], [PageId]) VALUES (215, 6, 260)
INSERT [dbo].[PageLayouts] ([Id], [TemplateId], [PageId]) VALUES (216, 6, 261)
INSERT [dbo].[PageLayouts] ([Id], [TemplateId], [PageId]) VALUES (218, 6, 263)
INSERT [dbo].[PageLayouts] ([Id], [TemplateId], [PageId]) VALUES (219, 1, 264)
SET IDENTITY_INSERT [dbo].[PageLayouts] OFF
/****** Object:  Table [dbo].[PageWidgets]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[InstanceId] [bigint] NULL,
	[ParentWidgetId] [bigint] NULL,
	[PageSection] [int] NOT NULL,
	[ColumnNumber] [int] NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[UserId] [bigint] NULL,
	[PageId] [bigint] NOT NULL,
	[WidgetId] [bigint] NULL,
	[TemplateWidgetId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[PageWidgets] ON
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (585, NULL, NULL, 1, 1, 1, 1, 256, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (586, 1, NULL, 2, 1, 1, 1, 256, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (588, 1, NULL, 1, 1, 2, 1, 254, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (590, 2, NULL, 1, 1, 2, 1, 255, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (592, 3, NULL, 1, 1, 1, 1, 254, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (593, 4, NULL, 1, 1, 1, 1, 258, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (594, 2, NULL, 1, 1, 2, 1, 258, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (595, 3, NULL, 1, 2, 1, 1, 255, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (596, 4, NULL, 1, 3, 1, 1, 255, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (608, 1, NULL, 1, 1, 1, 1, 260, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (611, 1, NULL, 0, 1, 6, 1, 261, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (643, NULL, NULL, 1, 1, 3, 1, 263, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (645, 6, NULL, 1, 1, 2, 1, 263, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (646, NULL, NULL, 1, 1, 1, 1, 255, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (648, NULL, NULL, 1, 1, 1, 1, 263, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (649, 1, NULL, 1, 1, 1, NULL, 264, NULL, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (650, NULL, NULL, 1, 1, 3, 1, 261, 281, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (651, NULL, NULL, 1, 1, 2, 1, 261, 261, NULL)
INSERT [dbo].[PageWidgets] ([Id], [InstanceId], [ParentWidgetId], [PageSection], [ColumnNumber], [OrderNumber], [UserId], [PageId], [WidgetId], [TemplateWidgetId]) VALUES (655, NULL, NULL, 1, 1, 1, 1, 261, 280, NULL)
SET IDENTITY_INSERT [dbo].[PageWidgets] OFF
/****** Object:  Table [dbo].[PageSettings]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
SET IDENTITY_INSERT [dbo].[PageSettings] ON
INSERT [dbo].[PageSettings] ([Id], [CustomCss], [LookAndFeelSettingsId], [PageId]) VALUES (32, N'
#widget_588{
margin-top:10px;
}', 55, 254)
INSERT [dbo].[PageSettings] ([Id], [CustomCss], [LookAndFeelSettingsId], [PageId]) VALUES (34, NULL, 59, 261)
INSERT [dbo].[PageSettings] ([Id], [CustomCss], [LookAndFeelSettingsId], [PageId]) VALUES (35, N'
.widget .form_i{
float:left; width: 100px; margin-right:10px;
}

.widget .form_i input[type="text"]{
width:100px !important;
margin-top:0;
}

.widget .form_i .w_365 {
width:100px !important;
}
.widget .form_i label {
display:none;}
.widget #RememberMe {
display:none;}


.widget .login-holder .form_i{
float:left; width: 100px; margin-right:10px;
}

.widget .login-holder .form_i input[type="text"]{
width:100px !important;
margin-top:0;
}

.widget .login-holder .form_i .w_365 {
width:100px !important;
}
.widget .login-holder .form_i label {
display:none;}
.widget .login-holder #RememberMe {
display:none;}
', 61, 263)
SET IDENTITY_INSERT [dbo].[PageSettings] OFF
/****** Object:  Table [dbo].[WebContent_Articles]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebContent_Articles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UserId] [bigint] NULL,
	[Status] [int] NOT NULL,
	[Author] [nvarchar](255) NULL,
	[StartPublishingDate] [datetime] NULL,
	[FinishPublishingDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL,
	[Url] [nvarchar](255) NOT NULL,
	[UrlType] [int] NOT NULL,
	[CategoryId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebContent_ArticleLocales]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebContent_ArticleLocales](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Summary] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Culture] [nvarchar](10) NULL,
	[ArticleId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebContent_ArticleFiles]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebContent_ArticleFiles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[ArticleId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageWidgetSettings]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
SET IDENTITY_INSERT [dbo].[PageWidgetSettings] ON
INSERT [dbo].[PageWidgetSettings] ([Id], [CustomCssClasses], [LookAndFeelSettingsId], [WidgetId]) VALUES (13, NULL, 54, 588)
INSERT [dbo].[PageWidgetSettings] ([Id], [CustomCssClasses], [LookAndFeelSettingsId], [WidgetId]) VALUES (15, NULL, 58, 611)
SET IDENTITY_INSERT [dbo].[PageWidgetSettings] OFF
/****** Object:  Table [dbo].[PageLayoutColumnWidthValues]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
SET IDENTITY_INSERT [dbo].[PageLayoutColumnWidthValues] ON
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (90, 71, NULL, 2, 210)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (91, 29, NULL, 3, 210)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (92, 100, NULL, 4, 210)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (93, 71, NULL, 5, 210)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (94, 29, NULL, 6, 210)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (99, 25, NULL, 2, 215)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (100, 75, NULL, 3, 215)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (101, 100, NULL, 14, 215)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (102, 29, NULL, 15, 215)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (103, 39, NULL, 16, 215)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (104, 32, NULL, 17, 215)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (105, 100, NULL, 18, 215)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (106, 100, NULL, 14, 216)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (107, 33, NULL, 15, 216)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (108, 33, NULL, 16, 216)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (109, 34, NULL, 17, 216)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (110, 100, NULL, 18, 216)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (111, 100, NULL, 14, 218)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (112, 33, NULL, 15, 218)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (113, 33, NULL, 16, 218)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (114, 34, NULL, 17, 218)
INSERT [dbo].[PageLayoutColumnWidthValues] ([Id], [WidthValue], [Colspan], [ColumnId], [PageLayoutId]) VALUES (115, 100, NULL, 18, 218)
SET IDENTITY_INSERT [dbo].[PageLayoutColumnWidthValues] OFF
/****** Object:  Table [dbo].[Forms_FormWidgetAnswers]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forms_FormWidgetAnswers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[FormWidgetId] [bigint] NOT NULL,
	[UserId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forms_FormElementLocales]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forms_FormElementLocales](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[ElementValues] [nvarchar](max) NULL,
	[Culture] [nvarchar](10) NULL,
	[FormElementId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Forms_FormElementLocales] ON
INSERT [dbo].[Forms_FormElementLocales] ([Id], [Title], [ElementValues], [Culture], [FormElementId]) VALUES (1, N'Name', NULL, NULL, 1)
INSERT [dbo].[Forms_FormElementLocales] ([Id], [Title], [ElementValues], [Culture], [FormElementId]) VALUES (2, N'Message', NULL, NULL, 2)
INSERT [dbo].[Forms_FormElementLocales] ([Id], [Title], [ElementValues], [Culture], [FormElementId]) VALUES (3, N'Captcha', NULL, NULL, 3)
SET IDENTITY_INSERT [dbo].[Forms_FormElementLocales] OFF
/****** Object:  Table [dbo].[WebContent_WebContentWidgets]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebContent_WebContentWidgets](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ShowPagination] [bit] NOT NULL,
	[ItemsNumber] [int] NULL,
	[ViewMode] [int] NOT NULL,
	[ArticleId] [bigint] NULL,
	[SectionId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebContent_WebContentWidgetCategories]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebContent_WebContentWidgetCategories](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WebContentWidgetId] [bigint] NOT NULL,
	[CategoryId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forms_FormAnswerValues]    Script Date: 03/02/2012 17:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forms_FormAnswerValues](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Field] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[FormAnswerId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF__Breadcrum__ShowH__5C979F60]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[BreadcrumbsWidgets] ADD  DEFAULT ((0)) FOR [ShowHomePage]
GO
/****** Object:  Default [DF__SchemaTmp__Key__68336F3E]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[SchemaInfo] ADD  DEFAULT ('') FOR [Key]
GO
/****** Object:  Default [DF__SiteMapWi__Inclu__56DEC60A]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[SiteMapWidgets] ADD  DEFAULT ((0)) FOR [IncludeRootInTree]
GO
/****** Object:  Default [DF__Forms_For__IsReq__68DD7AB4]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Forms_FormElements] ADD  DEFAULT ((0)) FOR [IsRequired]
GO
/****** Object:  ForeignKey [FK_UserGroupsToRoles_FK_UserGroupsToRoles_Roles]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[UserGroupsToRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupsToRoles_FK_UserGroupsToRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserGroupsToRoles] CHECK CONSTRAINT [FK_UserGroupsToRoles_FK_UserGroupsToRoles_Roles]
GO
/****** Object:  ForeignKey [FK_UserGroupsToRoles_FK_UserGroupsToRoles_UserGroups]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[UserGroupsToRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupsToRoles_FK_UserGroupsToRoles_UserGroups] FOREIGN KEY([UserGroupId])
REFERENCES [dbo].[UserGroups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserGroupsToRoles] CHECK CONSTRAINT [FK_UserGroupsToRoles_FK_UserGroupsToRoles_UserGroups]
GO
/****** Object:  ForeignKey [FK_UserGroupsMembers_FK_UserGroupsMembers_UserGroups]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[UserGroupsMembers]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupsMembers_FK_UserGroupsMembers_UserGroups] FOREIGN KEY([UserGroupId])
REFERENCES [dbo].[UserGroups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserGroupsMembers] CHECK CONSTRAINT [FK_UserGroupsMembers_FK_UserGroupsMembers_UserGroups]
GO
/****** Object:  ForeignKey [FK_UserGroupsMembers_FK_UserGroupsMembers_Users]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[UserGroupsMembers]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupsMembers_FK_UserGroupsMembers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserGroupsMembers] CHECK CONSTRAINT [FK_UserGroupsMembers_FK_UserGroupsMembers_Users]
GO
/****** Object:  ForeignKey [FK_WebContent_Categories_Sections]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[WebContent_Categories]  WITH CHECK ADD  CONSTRAINT [FK_WebContent_Categories_Sections] FOREIGN KEY([SectionId])
REFERENCES [dbo].[WebContent_Sections] ([Id])
GO
ALTER TABLE [dbo].[WebContent_Categories] CHECK CONSTRAINT [FK_WebContent_Categories_Sections]
GO
/****** Object:  ForeignKey [FK_RoleLocales_Roles]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[RoleLocales]  WITH CHECK ADD  CONSTRAINT [FK_RoleLocales_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleLocales] CHECK CONSTRAINT [FK_RoleLocales_Roles]
GO
/****** Object:  ForeignKey [FK_PluginLocales_Plugins]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PluginLocales]  WITH CHECK ADD  CONSTRAINT [FK_PluginLocales_Plugins] FOREIGN KEY([PluginId])
REFERENCES [dbo].[Plugins] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PluginLocales] CHECK CONSTRAINT [FK_PluginLocales_Plugins]
GO
/****** Object:  ForeignKey [FK_Permissions_FK_Permissions_EntityTypes]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_FK_Permissions_EntityTypes] FOREIGN KEY([EntityTypeId])
REFERENCES [dbo].[EntityTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_FK_Permissions_EntityTypes]
GO
/****** Object:  ForeignKey [FK_Permissions_FK_Permissions_Users]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_FK_Permissions_Users] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_FK_Permissions_Users]
GO
/****** Object:  ForeignKey [FK_PageLayoutRows_PageLayoutRowsTemplates]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageLayoutRows]  WITH CHECK ADD  CONSTRAINT [FK_PageLayoutRows_PageLayoutRowsTemplates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[PageLayoutTemplates] ([Id])
GO
ALTER TABLE [dbo].[PageLayoutRows] CHECK CONSTRAINT [FK_PageLayoutRows_PageLayoutRowsTemplates]
GO
/****** Object:  ForeignKey [FK_Forms_Forms_FormUsers]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Forms_Forms]  WITH CHECK ADD  CONSTRAINT [FK_Forms_Forms_FormUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Forms_Forms] CHECK CONSTRAINT [FK_Forms_Forms_FormUsers]
GO
/****** Object:  ForeignKey [FK_Migrations_MigrationsPlugins]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Migrations]  WITH CHECK ADD  CONSTRAINT [FK_Migrations_MigrationsPlugins] FOREIGN KEY([PluginId])
REFERENCES [dbo].[Plugins] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Migrations] CHECK CONSTRAINT [FK_Migrations_MigrationsPlugins]
GO
/****** Object:  ForeignKey [FK_Pages_Pages1]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Pages1] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Pages] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Pages1]
GO
/****** Object:  ForeignKey [FK_Pages_PageUsers]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_PageUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_PageUsers]
GO
/****** Object:  ForeignKey [FK_Widgets_WidgetPlugins]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Widgets]  WITH CHECK ADD  CONSTRAINT [FK_Widgets_WidgetPlugins] FOREIGN KEY([PluginId])
REFERENCES [dbo].[Plugins] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Widgets] CHECK CONSTRAINT [FK_Widgets_WidgetPlugins]
GO
/****** Object:  ForeignKey [FK_WebContent_SectionSettings_Sections]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[WebContent_SectionSettings]  WITH CHECK ADD  CONSTRAINT [FK_WebContent_SectionSettings_Sections] FOREIGN KEY([SectionId])
REFERENCES [dbo].[WebContent_Sections] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WebContent_SectionSettings] CHECK CONSTRAINT [FK_WebContent_SectionSettings_Sections]
GO
/****** Object:  ForeignKey [FK_UsersToRoles_FK_UsersToRoles_Roles]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[UsersToRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersToRoles_FK_UsersToRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersToRoles] CHECK CONSTRAINT [FK_UsersToRoles_FK_UsersToRoles_Roles]
GO
/****** Object:  ForeignKey [FK_UsersToRoles_FK_UsersToRoles_Users]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[UsersToRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersToRoles_FK_UsersToRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersToRoles] CHECK CONSTRAINT [FK_UsersToRoles_FK_UsersToRoles_Users]
GO
/****** Object:  ForeignKey [FK_WebContent_SectionLocales_Sections]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[WebContent_SectionLocales]  WITH CHECK ADD  CONSTRAINT [FK_WebContent_SectionLocales_Sections] FOREIGN KEY([SectionId])
REFERENCES [dbo].[WebContent_Sections] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WebContent_SectionLocales] CHECK CONSTRAINT [FK_WebContent_SectionLocales_Sections]
GO
/****** Object:  ForeignKey [FK_SiteMapWidgets_SiteMapWidgetRootPages]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[SiteMapWidgets]  WITH CHECK ADD  CONSTRAINT [FK_SiteMapWidgets_SiteMapWidgetRootPages] FOREIGN KEY([RootPageId])
REFERENCES [dbo].[Pages] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[SiteMapWidgets] CHECK CONSTRAINT [FK_SiteMapWidgets_SiteMapWidgetRootPages]
GO
/****** Object:  ForeignKey [FK_WebContent_CategoryLocales_Categories]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[WebContent_CategoryLocales]  WITH CHECK ADD  CONSTRAINT [FK_WebContent_CategoryLocales_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[WebContent_Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WebContent_CategoryLocales] CHECK CONSTRAINT [FK_WebContent_CategoryLocales_Categories]
GO
/****** Object:  ForeignKey [FK_WidgetLocales_Widgets]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[WidgetLocales]  WITH CHECK ADD  CONSTRAINT [FK_WidgetLocales_Widgets] FOREIGN KEY([WidgetId])
REFERENCES [dbo].[Widgets] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WidgetLocales] CHECK CONSTRAINT [FK_WidgetLocales_Widgets]
GO
/****** Object:  ForeignKey [FK_PageLocales_Pages]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageLocales]  WITH CHECK ADD  CONSTRAINT [FK_PageLocales_Pages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageLocales] CHECK CONSTRAINT [FK_PageLocales_Pages]
GO
/****** Object:  ForeignKey [FK_PageLayoutColumns_PageLayoutColumnsRows]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageLayoutColumns]  WITH CHECK ADD  CONSTRAINT [FK_PageLayoutColumns_PageLayoutColumnsRows] FOREIGN KEY([RowId])
REFERENCES [dbo].[PageLayoutRows] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageLayoutColumns] CHECK CONSTRAINT [FK_PageLayoutColumns_PageLayoutColumnsRows]
GO
/****** Object:  ForeignKey [FK_ListMenuWidgetPages_ListMenuWidgetPagesPages]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[ListMenuWidgetPages]  WITH CHECK ADD  CONSTRAINT [FK_ListMenuWidgetPages_ListMenuWidgetPagesPages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ListMenuWidgetPages] CHECK CONSTRAINT [FK_ListMenuWidgetPages_ListMenuWidgetPagesPages]
GO
/****** Object:  ForeignKey [FK_ListMenuWidgetPages_ListMenuWidgetPagesWidgets]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[ListMenuWidgetPages]  WITH CHECK ADD  CONSTRAINT [FK_ListMenuWidgetPages_ListMenuWidgetPagesWidgets] FOREIGN KEY([ListMenuWidgetId])
REFERENCES [dbo].[ListMenuWidgets] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ListMenuWidgetPages] CHECK CONSTRAINT [FK_ListMenuWidgetPages_ListMenuWidgetPagesWidgets]
GO
/****** Object:  ForeignKey [FK_Forms_FormLocales_Forms]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Forms_FormLocales]  WITH CHECK ADD  CONSTRAINT [FK_Forms_FormLocales_Forms] FOREIGN KEY([formId])
REFERENCES [dbo].[Forms_Forms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Forms_FormLocales] CHECK CONSTRAINT [FK_Forms_FormLocales_Forms]
GO
/****** Object:  ForeignKey [FK_Forms_FormElements_FormElementForms]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Forms_FormElements]  WITH CHECK ADD  CONSTRAINT [FK_Forms_FormElements_FormElementForms] FOREIGN KEY([FormId])
REFERENCES [dbo].[Forms_Forms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Forms_FormElements] CHECK CONSTRAINT [FK_Forms_FormElements_FormElementForms]
GO
/****** Object:  ForeignKey [FK_Forms_FormsBuilderWidgets_FormsBuilders]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Forms_FormsBuilderWidgets]  WITH CHECK ADD  CONSTRAINT [FK_Forms_FormsBuilderWidgets_FormsBuilders] FOREIGN KEY([FormId])
REFERENCES [dbo].[Forms_Forms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Forms_FormsBuilderWidgets] CHECK CONSTRAINT [FK_Forms_FormsBuilderWidgets_FormsBuilders]
GO
/****** Object:  ForeignKey [FK_Forms_FormsBuilderWidgets_FormsBuilderWidgetUsers]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Forms_FormsBuilderWidgets]  WITH CHECK ADD  CONSTRAINT [FK_Forms_FormsBuilderWidgets_FormsBuilderWidgetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Forms_FormsBuilderWidgets] CHECK CONSTRAINT [FK_Forms_FormsBuilderWidgets_FormsBuilderWidgetUsers]
GO
/****** Object:  ForeignKey [FK_PageLayouts_PageLayoutsPages]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageLayouts]  WITH CHECK ADD  CONSTRAINT [FK_PageLayouts_PageLayoutsPages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageLayouts] CHECK CONSTRAINT [FK_PageLayouts_PageLayoutsPages]
GO
/****** Object:  ForeignKey [FK_PageLayouts_PageLayoutsTemplates]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageLayouts]  WITH CHECK ADD  CONSTRAINT [FK_PageLayouts_PageLayoutsTemplates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[PageLayoutTemplates] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageLayouts] CHECK CONSTRAINT [FK_PageLayouts_PageLayoutsTemplates]
GO
/****** Object:  ForeignKey [FK_PageWidgets_PageWidgets]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageWidgets]  WITH CHECK ADD  CONSTRAINT [FK_PageWidgets_PageWidgets] FOREIGN KEY([TemplateWidgetId])
REFERENCES [dbo].[PageWidgets] ([Id])
GO
ALTER TABLE [dbo].[PageWidgets] CHECK CONSTRAINT [FK_PageWidgets_PageWidgets]
GO
/****** Object:  ForeignKey [FK_PageWidgets_PageWidgetsPages]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageWidgets]  WITH CHECK ADD  CONSTRAINT [FK_PageWidgets_PageWidgetsPages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageWidgets] CHECK CONSTRAINT [FK_PageWidgets_PageWidgetsPages]
GO
/****** Object:  ForeignKey [FK_PageWidgets_PageWidgetsUsers]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageWidgets]  WITH CHECK ADD  CONSTRAINT [FK_PageWidgets_PageWidgetsUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[PageWidgets] CHECK CONSTRAINT [FK_PageWidgets_PageWidgetsUsers]
GO
/****** Object:  ForeignKey [FK_PageWidgets_PageWidgetsWidgets]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageWidgets]  WITH CHECK ADD  CONSTRAINT [FK_PageWidgets_PageWidgetsWidgets] FOREIGN KEY([WidgetId])
REFERENCES [dbo].[Widgets] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[PageWidgets] CHECK CONSTRAINT [FK_PageWidgets_PageWidgetsWidgets]
GO
/****** Object:  ForeignKey [FK_PageSettings_PageSettingsLookAndFeels]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageSettings]  WITH CHECK ADD  CONSTRAINT [FK_PageSettings_PageSettingsLookAndFeels] FOREIGN KEY([LookAndFeelSettingsId])
REFERENCES [dbo].[LookAndFeelSettings] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageSettings] CHECK CONSTRAINT [FK_PageSettings_PageSettingsLookAndFeels]
GO
/****** Object:  ForeignKey [FK_PageSettings_PageSettingsPages]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageSettings]  WITH CHECK ADD  CONSTRAINT [FK_PageSettings_PageSettingsPages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageSettings] CHECK CONSTRAINT [FK_PageSettings_PageSettingsPages]
GO
/****** Object:  ForeignKey [FK_WebContent_Articles_Categories]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[WebContent_Articles]  WITH CHECK ADD  CONSTRAINT [FK_WebContent_Articles_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[WebContent_Categories] ([Id])
GO
ALTER TABLE [dbo].[WebContent_Articles] CHECK CONSTRAINT [FK_WebContent_Articles_Categories]
GO
/****** Object:  ForeignKey [FK_WebContent_ArticleLocales_Articles]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[WebContent_ArticleLocales]  WITH CHECK ADD  CONSTRAINT [FK_WebContent_ArticleLocales_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[WebContent_Articles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WebContent_ArticleLocales] CHECK CONSTRAINT [FK_WebContent_ArticleLocales_Articles]
GO
/****** Object:  ForeignKey [FK_WebContent_ArticleFiles_Articles]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[WebContent_ArticleFiles]  WITH CHECK ADD  CONSTRAINT [FK_WebContent_ArticleFiles_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[WebContent_Articles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WebContent_ArticleFiles] CHECK CONSTRAINT [FK_WebContent_ArticleFiles_Articles]
GO
/****** Object:  ForeignKey [FK_PageWidgetSettings_PageWidgetSettingsLookAndFeels]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageWidgetSettings]  WITH CHECK ADD  CONSTRAINT [FK_PageWidgetSettings_PageWidgetSettingsLookAndFeels] FOREIGN KEY([LookAndFeelSettingsId])
REFERENCES [dbo].[LookAndFeelSettings] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageWidgetSettings] CHECK CONSTRAINT [FK_PageWidgetSettings_PageWidgetSettingsLookAndFeels]
GO
/****** Object:  ForeignKey [FK_PageWidgetSettings_PageWidgetSettingsWidgets]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageWidgetSettings]  WITH CHECK ADD  CONSTRAINT [FK_PageWidgetSettings_PageWidgetSettingsWidgets] FOREIGN KEY([WidgetId])
REFERENCES [dbo].[PageWidgets] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageWidgetSettings] CHECK CONSTRAINT [FK_PageWidgetSettings_PageWidgetSettingsWidgets]
GO
/****** Object:  ForeignKey [FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesColumns]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageLayoutColumnWidthValues]  WITH CHECK ADD  CONSTRAINT [FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesColumns] FOREIGN KEY([ColumnId])
REFERENCES [dbo].[PageLayoutColumns] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageLayoutColumnWidthValues] CHECK CONSTRAINT [FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesColumns]
GO
/****** Object:  ForeignKey [FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesLayouts]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[PageLayoutColumnWidthValues]  WITH CHECK ADD  CONSTRAINT [FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesLayouts] FOREIGN KEY([PageLayoutId])
REFERENCES [dbo].[PageLayouts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageLayoutColumnWidthValues] CHECK CONSTRAINT [FK_PageLayoutColumnWidthValues_PageLayoutColumnWidthValuesLayouts]
GO
/****** Object:  ForeignKey [FK_Forms_FormWidgetAnswers_AnswerUsers]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Forms_FormWidgetAnswers]  WITH CHECK ADD  CONSTRAINT [FK_Forms_FormWidgetAnswers_AnswerUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Forms_FormWidgetAnswers] CHECK CONSTRAINT [FK_Forms_FormWidgetAnswers_AnswerUsers]
GO
/****** Object:  ForeignKey [FK_Forms_FormWidgetAnswers_FormWidgets]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Forms_FormWidgetAnswers]  WITH CHECK ADD  CONSTRAINT [FK_Forms_FormWidgetAnswers_FormWidgets] FOREIGN KEY([FormWidgetId])
REFERENCES [dbo].[Forms_FormsBuilderWidgets] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Forms_FormWidgetAnswers] CHECK CONSTRAINT [FK_Forms_FormWidgetAnswers_FormWidgets]
GO
/****** Object:  ForeignKey [FK_Forms_FormElementLocales_FormElements]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Forms_FormElementLocales]  WITH CHECK ADD  CONSTRAINT [FK_Forms_FormElementLocales_FormElements] FOREIGN KEY([FormElementId])
REFERENCES [dbo].[Forms_FormElements] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Forms_FormElementLocales] CHECK CONSTRAINT [FK_Forms_FormElementLocales_FormElements]
GO
/****** Object:  ForeignKey [FK_WebContent_WebContentWidgets_Articles]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[WebContent_WebContentWidgets]  WITH CHECK ADD  CONSTRAINT [FK_WebContent_WebContentWidgets_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[WebContent_Articles] ([Id])
GO
ALTER TABLE [dbo].[WebContent_WebContentWidgets] CHECK CONSTRAINT [FK_WebContent_WebContentWidgets_Articles]
GO
/****** Object:  ForeignKey [FK_WebContent_WebContentWidgets_Sections]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[WebContent_WebContentWidgets]  WITH CHECK ADD  CONSTRAINT [FK_WebContent_WebContentWidgets_Sections] FOREIGN KEY([SectionId])
REFERENCES [dbo].[WebContent_Sections] ([Id])
GO
ALTER TABLE [dbo].[WebContent_WebContentWidgets] CHECK CONSTRAINT [FK_WebContent_WebContentWidgets_Sections]
GO
/****** Object:  ForeignKey [FK_WebContent_WebContentWidgetCategories_Categories]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[WebContent_WebContentWidgetCategories]  WITH CHECK ADD  CONSTRAINT [FK_WebContent_WebContentWidgetCategories_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[WebContent_Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WebContent_WebContentWidgetCategories] CHECK CONSTRAINT [FK_WebContent_WebContentWidgetCategories_Categories]
GO
/****** Object:  ForeignKey [FK_WebContent_WebContentWidgetCategories_WebContentWidgets]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[WebContent_WebContentWidgetCategories]  WITH CHECK ADD  CONSTRAINT [FK_WebContent_WebContentWidgetCategories_WebContentWidgets] FOREIGN KEY([WebContentWidgetId])
REFERENCES [dbo].[WebContent_WebContentWidgets] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WebContent_WebContentWidgetCategories] CHECK CONSTRAINT [FK_WebContent_WebContentWidgetCategories_WebContentWidgets]
GO
/****** Object:  ForeignKey [FK_Forms_FormAnswerValues_FormAnswers]    Script Date: 03/02/2012 17:16:26 ******/
ALTER TABLE [dbo].[Forms_FormAnswerValues]  WITH CHECK ADD  CONSTRAINT [FK_Forms_FormAnswerValues_FormAnswers] FOREIGN KEY([FormAnswerId])
REFERENCES [dbo].[Forms_FormWidgetAnswers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Forms_FormAnswerValues] CHECK CONSTRAINT [FK_Forms_FormAnswerValues_FormAnswers]
GO
