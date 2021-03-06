USE [ava]
GO
/****** Object:  Table [dbo].[rfcNatureOfBusiness]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[rfcNatureOfBusiness]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[rfcNatureOfBusiness]
GO
/****** Object:  Table [dbo].[rfcProductBrands]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[rfcProductBrands]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[rfcProductBrands]
GO
/****** Object:  Table [dbo].[rfcProductCategory]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[rfcProductCategory]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[rfcProductCategory]
GO
/****** Object:  Table [dbo].[rfcProductSubCategory]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[rfcProductSubCategory]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[rfcProductSubCategory]
GO
/****** Object:  Table [dbo].[rfcUserTypes]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[rfcUserTypes]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[rfcUserTypes]
GO
/****** Object:  Table [dbo].[tblComments]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblComments]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblComments]
GO
/****** Object:  Table [dbo].[tblDnbFinancialReport]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblDnbFinancialReport]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblDnbFinancialReport]
GO
/****** Object:  Table [dbo].[tblDnbLegalReport]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblDnbLegalReport]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblDnbLegalReport]
GO
/****** Object:  Table [dbo].[tblDnbRating]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblDnbRating]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblDnbRating]
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblUsers]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblUsers]
GO
/****** Object:  Table [dbo].[tblUserTypes]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblUserTypes]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblUserTypes]
GO
/****** Object:  Table [dbo].[tblVendorApplicants]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorApplicants]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorApplicants]
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyFAALFinance]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorApprovalbyFAALFinance]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorApprovalbyFAALFinance]
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyFAALogistics]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorApprovalbyFAALogistics]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorApprovalbyFAALogistics]
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyLegal]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorApprovalbyLegal]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorApprovalbyLegal]
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyVmIssue]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorApprovalbyVmIssue]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorApprovalbyVmIssue]
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyVmReco]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorApprovalbyVmReco]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorApprovalbyVmReco]
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyVmTech]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorApprovalbyVmTech]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorApprovalbyVmTech]
GO
/****** Object:  Table [dbo].[tblVendorBackOnKeyPersonnel]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorBackOnKeyPersonnel]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorBackOnKeyPersonnel]
GO
/****** Object:  Table [dbo].[tblVendorBankInformation]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorBankInformation]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorBankInformation]
GO
/****** Object:  Table [dbo].[tblVendorBoardMembers]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorBoardMembers]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorBoardMembers]
GO
/****** Object:  Table [dbo].[tblVendorBranches]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorBranches]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorBranches]
GO
/****** Object:  Table [dbo].[tblVendorCertifications]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorCertifications]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorCertifications]
GO
/****** Object:  Table [dbo].[tblVendorConflictOfInterest]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorConflictOfInterest]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorConflictOfInterest]
GO
/****** Object:  Table [dbo].[tblVendorFinancialInformation]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorFinancialInformation]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorFinancialInformation]
GO
/****** Object:  Table [dbo].[tblVendorIncidentReport]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorIncidentReport]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorIncidentReport]
GO
/****** Object:  Table [dbo].[tblVendorInformation]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorInformation]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorInformation]
GO
/****** Object:  Table [dbo].[tblVendorInsuranceInformation]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorInsuranceInformation]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorInsuranceInformation]
GO
/****** Object:  Table [dbo].[tblVendorLegalCompliance]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorLegalCompliance]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorLegalCompliance]
GO
/****** Object:  Table [dbo].[tblVendorNatureOfBusiness]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorNatureOfBusiness]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorNatureOfBusiness]
GO
/****** Object:  Table [dbo].[tblVendorProductsAndServices]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorProductsAndServices]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorProductsAndServices]
GO
/****** Object:  Table [dbo].[tblVendorRegulatoryRequirements]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorRegulatoryRequirements]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorRegulatoryRequirements]
GO
/****** Object:  Table [dbo].[tblVendorShareHolders]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorShareHolders]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorShareHolders]
GO
/****** Object:  Table [dbo].[tblVendorSubsidiaries]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorSubsidiaries]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorSubsidiaries]
GO
/****** Object:  Table [dbo].[tblVendorSupplierReferences]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorSupplierReferences]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorSupplierReferences]
GO
/****** Object:  Table [dbo].[tblVendorSuppliersDeclaration]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorSuppliersDeclaration]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorSuppliersDeclaration]
GO
/****** Object:  Table [dbo].[tblVendorTopCompetitors]    Script Date: 03/29/2012 14:08:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[tblVendorTopCompetitors]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[tblVendorTopCompetitors]