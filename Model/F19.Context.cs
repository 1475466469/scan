﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


namespace Model
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class DCF19Entities : DbContext
{
    public DCF19Entities()
        : base("name=DCF19Entities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<t_ADMM_UsrMst> t_ADMM_UsrMst { get; set; }

    public virtual DbSet<t_ADMM_UsrDataRightItem> t_ADMM_UsrDataRightItem { get; set; }

    public virtual DbSet<t_ADMM_UsrDataRightMst> t_ADMM_UsrDataRightMst { get; set; }

    public virtual DbSet<t_ADMM_UsrItem> t_ADMM_UsrItem { get; set; }

    public virtual DbSet<t_BOMM_GoodsMst> t_BOMM_GoodsMst { get; set; }

    public virtual DbSet<t_INVD_StkOutLogMst> t_INVD_StkOutLogMst { get; set; }

    public virtual DbSet<t_COPD_DlvMst> t_COPD_DlvMst { get; set; }

    public virtual DbSet<t_CRMM_CstMst> t_CRMM_CstMst { get; set; }

    public virtual DbSet<V_INVD_StkOutLogItemSum> V_INVD_StkOutLogItemSum { get; set; }

    public virtual DbSet<t_BCMM_BarcodeFgItem> t_BCMM_BarcodeFgItem { get; set; }

    public virtual DbSet<t_BCMM_BarcodeItem> t_BCMM_BarcodeItem { get; set; }

    public virtual DbSet<t_BCMM_BarcodeMst> t_BCMM_BarcodeMst { get; set; }

    public virtual DbSet<DO_t_TemporaryScan> DO_t_TemporaryScan { get; set; }

    public virtual DbSet<t_INVD_StkOutLogItem> t_INVD_StkOutLogItem { get; set; }

    public virtual DbSet<Do_t_dious_Scan> Do_t_dious_Scan { get; set; }

}

}

