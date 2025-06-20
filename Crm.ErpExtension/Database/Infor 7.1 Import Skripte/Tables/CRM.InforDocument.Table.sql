/****** Object:  Table [CRM].[InforDocument]    Script Date: 07/28/2011 09:58:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CRM].[InforDocument]') AND type in (N'U'))
DROP TABLE [CRM].[InforDocument]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CRM].[InforDocument](
	[DocumentType] [nvarchar](50) NOT NULL,
	[DocumentState] [nvarchar](50) NULL,
	[DocumentServiceType] [nvarchar](50) NULL,
	[DocumentServiceState] [nvarchar](50) NULL,
	[CompanyNo] [nvarchar](50) NOT NULL,
	[RecordId] [int] NOT NULL,
	[RecordType] [int] NOT NULL,
	[OrderNo] [nvarchar](50) NOT NULL,
	[QuoteNo] [nvarchar](50) NULL,
	[QuoteDate] [datetime] NULL,
	[RequestNo] [nvarchar](50) NULL,
	[RequestDate] [datetime] NULL,
	[OrderConfirmationNo] [nvarchar](50) NULL,
	[OrderConfirmationDate] [datetime] NULL,
	[DeliverNoteNo] [nvarchar](50) NULL,
	[DeliveryNoteDate] [datetime] NULL,
	[InvoiceNo] [nvarchar](50) NULL,
	[InvoiceDate] [datetime] NULL,
	[DocumentDate11] [datetime] NULL,
	[ItemNo] [nvarchar](50) NULL,
	[Total] [decimal](18, 0) NULL,
	[Total wo Taxes] [decimal](18, 0) NULL,
	[Currency] [nvarchar](50) NULL,
	[State] [int] NULL,
	[Commission] [nvarchar](200) NULL,
	[DueDate] [datetime] NULL,
	[DeviceNo] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
