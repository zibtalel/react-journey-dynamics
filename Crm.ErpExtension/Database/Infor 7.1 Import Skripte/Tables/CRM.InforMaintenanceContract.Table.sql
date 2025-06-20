/****** Object:  Table [CRM].[InforMaintenanceContract]    Script Date: 07/28/2011 09:58:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CRM].[InforMaintenanceContract]') AND type in (N'U'))
DROP TABLE [CRM].[InforMaintenanceContract]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CRM].[InforMaintenanceContract](
	[ContractNo] [nvarchar](50) NOT NULL,
	[CustomerNo] [nvarchar](50) NOT NULL,
	[MachineNo] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NULL,
	[BeginDate] [datetime] NULL,
	[ExpirationDate] [datetime] NULL,
	[ServiceLevelAgreementKey] [nvarchar](50) NULL,
	[ServiceLevelDescription] [nvarchar](200) NULL,
	[LastServiceDate] [datetime] NULL,
	[NextServiceDate] [datetime] NULL,
	[LatestServiceDate] [datetime] NULL,
	[Total] [decimal](18, 0) NULL,
	[Currency] [nvarchar](50) NULL,
	[Date] [datetime] NULL,
	[DocumentState] [nvarchar](50) NULL,
	[Remark] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ContractNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
