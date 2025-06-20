/****** Object:  Table [CRM].[InforEquipmentFile]    Script Date: 07/28/2011 09:58:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CRM].[InforEquipmentFile]') AND type in (N'U'))
DROP TABLE [CRM].[InforEquipmentFile]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CRM].[InforEquipmentFile](
	[MachineNo] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[CustomerNo] [nvarchar](200) NULL,
	[Commission] [nvarchar](200) NULL,
	[SerialNo] [nvarchar](200) NULL,
	[AssemblyDate] [datetime] NULL,
	[WarrantyFrom] [datetime] NULL,
	[WarrantyUntil] [datetime] NULL,
	[State] [nvarchar](50) NULL,
	[InitialOperationDate] [datetime] NULL,
	[OperatingHours] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MachineNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
