INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'd7f87100-0b32-455a-9c27-f4acc6e029ad', N'admin', N'admin', N'9f7031ee-581e-43ef-910f-0c234c53206a')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3c3cef89-53d3-443d-b2cd-9f55a65848e3', N'power@skp.com', N'POWER@SKP.COM', N'power@skp.com', N'POWER@SKP.COM', 1, N'AQAAAAEAACcQAAAAEDELg4ROspBsJBiVRUzNuLFQ/lSleI001yL7CRTsaQ+l5ZnaswKxaMhY1voUiCratQ==', N'THDS3L7ZZTAEREQLVY463QPELOTVQXM4', N'9c2b536a-6fda-41ad-a48e-d5631112e2af', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3c3cef89-53d3-443d-b2cd-9f55a65848e3', N'd7f87100-0b32-455a-9c27-f4acc6e029ad')
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (1, N'Asp Dot Net With C#')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (2, N'Python')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (3, N'J2SE (Core Java)')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (4, N'English Language')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (5, N'Operating System')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (6, N'C Language')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (7, N'C++ Language')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[BooksDetails] ON 
GO
INSERT [dbo].[BooksDetails] ([BookID], [BookName], [Author], [Publisher], [BookDescription], [Extension], [CategoryID]) VALUES (2, N'English Language Communication Skills', N'Urmila Rai', N'Himalaya Publication', N'A Books for Improve English Language Communication Skills ', N'.pdf', 4)
GO
INSERT [dbo].[BooksDetails] ([BookID], [BookName], [Author], [Publisher], [BookDescription], [Extension], [CategoryID]) VALUES (3, N'English Grammer', N'Raymond Murphy', N'Cambride University', N'A Books for Improve English Grammer', N'.pdf', 4)
GO
INSERT [dbo].[BooksDetails] ([BookID], [BookName], [Author], [Publisher], [BookDescription], [Extension], [CategoryID]) VALUES (4, N'Learning English as a Foreign Language for Dummies ', N'Gavin Dudeney', N'Wiley', N'Learning English as a Foreign Language for Dummies ', N'.pdf', 4)
GO
INSERT [dbo].[BooksDetails] ([BookID], [BookName], [Author], [Publisher], [BookDescription], [Extension], [CategoryID]) VALUES (5, N'Learn C Programming', N'Tutorials Points', N'Tutorials Points', N'Learn Basic C Programming', N'.pdf', 6)
GO
INSERT [dbo].[BooksDetails] ([BookID], [BookName], [Author], [Publisher], [BookDescription], [Extension], [CategoryID]) VALUES (7, N'C Programming', N'Harry A. Chaudhary', N'First MIT- Createspace Inc.', N'Learn Basic to Advance C Programming', N'.pdf', 6)
GO
INSERT [dbo].[BooksDetails] ([BookID], [BookName], [Author], [Publisher], [BookDescription], [Extension], [CategoryID]) VALUES (8, N'PYTHON CRASH COURSE', N'Eric Matthers', N'William Pollock', N'Easy way to Learn with PYTHON CRASH COURSE', N'.pdf', 2)
GO
INSERT [dbo].[BooksDetails] ([BookID], [BookName], [Author], [Publisher], [BookDescription], [Extension], [CategoryID]) VALUES (9, N'Python 3', N'Tutorials Points', N'Tutorials Points', N'A Books for Python Basics', N'.pdf', 2)
GO
INSERT [dbo].[BooksDetails] ([BookID], [BookName], [Author], [Publisher], [BookDescription], [Extension], [CategoryID]) VALUES (10, N'Mastering ASP.NET', N'A. Russell Jones', N'Richard Mills', N'Asp.net with Ado Dot Net', N'.pdf', 1)
GO
INSERT [dbo].[BooksDetails] ([BookID], [BookName], [Author], [Publisher], [BookDescription], [Extension], [CategoryID]) VALUES (11, N'Microsoft Asp.net 4', N'George Shepherd', N'Microsoft Press', N'Web Application Basic with ADO.net', N'.pdf', 1)
GO
INSERT [dbo].[BooksDetails] ([BookID], [BookName], [Author], [Publisher], [BookDescription], [Extension], [CategoryID]) VALUES (12, N'Operating System Concepts and Basic Linux Commands', N'Shital Vivek Ghate', N'EDUCREATION PUBLISHING', N'Operating System Concepts and Basic Linux Commands', N'.pdf', 5)
GO
SET IDENTITY_INSERT [dbo].[BooksDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[NotesDetail] ON 
GO
INSERT [dbo].[NotesDetail] ([NotesID], [NameName], [Author], [NotesDescription], [Extension], [CategoryID]) VALUES (1, N'Pointers', N'Simple Learns', N'Good Tutorial About ', N'.pdf', 1)
GO
SET IDENTITY_INSERT [dbo].[NotesDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[VideoLecDetail] ON 
GO
INSERT [dbo].[VideoLecDetail] ([VidID], [VidName], [Author], [VidDescription], [Extension], [CategoryID]) VALUES (1, N'Python INtroudction', N'Ben Stone', N'Good Video', N'.mp4', 2)
GO
SET IDENTITY_INSERT [dbo].[VideoLecDetail] OFF
GO

