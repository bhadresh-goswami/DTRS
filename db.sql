CREATE TABLE [dbo].[RoleMaster] (
    [RoleId]    INT           NOT NULL,
    [RoleTitle] VARCHAR (200) NOT NULL,
    [IsEnabled] BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC),
    UNIQUE NONCLUSTERED ([RoleTitle] ASC)
);

CREATE TABLE [dbo].[UserLoginMaster] (
    [LoginId]        INT           NOT NULL,
    [RocketUserName] VARCHAR (300) NOT NULL,
    [EmailId]        VARCHAR (MAX) NOT NULL,
    [Password]       VARCHAR (40)  NOT NULL,
    [IsEnabled]      BIT           NOT NULL,
    [IsLogin]        BIT           NOT NULL,
    [UserRole]       INT           NOT NULL,
    [Location]       VARCHAR (100) NULL,
    [FullName]       VARCHAR (100) NULL,
    [ImageName]      VARCHAR (300) NULL,
    PRIMARY KEY CLUSTERED ([LoginId] ASC),
    UNIQUE NONCLUSTERED ([RocketUserName] ASC),
    FOREIGN KEY ([UserRole]) REFERENCES [dbo].[RoleMaster] ([RoleId]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[TechnologyMaster] (
    [TechId]   INT           NOT NULL,
    [TechName] VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([TechId] ASC)
);

CREATE TABLE [dbo].[TeamMaster] (
    [TID]         INT          IDENTITY (1, 1) NOT NULL,
    [UserName]    VARCHAR (50) NOT NULL,
    [TLName]      VARCHAR (50) NOT NULL,
    [ManagerName] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([TID] ASC)
);

CREATE TABLE [dbo].[JobPortalMaster] (
    [JPId]           INT           IDENTITY (1, 1) NOT NULL,
    [JobPortalTitle] VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([JPId] ASC),
    UNIQUE NONCLUSTERED ([JobPortalTitle] ASC)
);
CREATE TABLE [dbo].[SubmissionMaster] (
    [SId]               INT           IDENTITY (1, 1) NOT NULL,
    [SDate]             DATE          NOT NULL,
    [STime]             TIME (7)      NOT NULL,
    [SBy]               VARCHAR (200) NOT NULL,
    [CandidateName]     VARCHAR (300) NOT NULL,
    [Technology]        VARCHAR (400) NOT NULL,
    [Location]          VARCHAR (100) NOT NULL,
    [Rate]              MONEY         NOT NULL,
    [ClientName]        VARCHAR (MAX) NOT NULL,
    [VendorName]        VARCHAR (MAX) NOT NULL,
    [ContactEmailId]    VARCHAR (MAX) NOT NULL,
    [VendorCompanyName] VARCHAR (MAX) NOT NULL,
    [JobPortalName]     VARCHAR (MAX) NOT NULL,
	JobDescription varchar(max) null,
	InterviewStatus varchar(max) null,
	InterviewDate date null,
	AssingedTo varchar(100) not null,
	InterviewTime time null,
	InterviewFeedBack varchar(max) not null,
    PRIMARY KEY CLUSTERED ([SId] ASC)
);

CREATE TABLE [dbo].[InterviewMaster] (
    [IId]         INT           IDENTITY (1, 1) NOT NULL,
    [IDate]       DATE          NOT NULL,
    [ITime]       TIME (7)      NOT NULL,
    [Description] VARCHAR (MAX) NOT NULL,
    [RefSId]      INT           NOT NULL,
    [Status]      VARCHAR (100) NOT NULL,
    [AssignTo]    VARCHAR (100) NOT NULL,
    [FeedBack]    VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([IId] ASC),
    FOREIGN KEY ([RefSId]) REFERENCES [dbo].[SubmissionMaster] ([SId]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[CandidateMaster] (
    [CandidateId]      INT           NOT NULL,
    [CandidateName]    VARCHAR (200) NOT NULL,
    [CandidateEmailId] VARCHAR (500) NOT NULL,
    [MarketingEmailId] VARCHAR (500) NOT NULL,
    [ContactNumber]    VARCHAR (20)  NULL,
    [MarketingNumver]  VARCHAR (20)  NULL,
    [InsertBy]         VARCHAR (100) NULL,
    [Technology]       VARCHAR (100) NOT NULL,
    [AssignTo]         VARCHAR (100) NULL,
    [VisaStatus]       VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([CandidateId] ASC)
);



