using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Aplicant_Chnage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    countryid = table.Column<string>(type: "text", nullable: false),
                    countrycode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    dialcode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    countryname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    countryshortname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    cultureinfo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    currencyname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    currencysymbol = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    currencyshortname = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    flagclass = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.countryid);
                });

            migrationBuilder.CreateTable(
                name: "companys",
                columns: table => new
                {
                    comid = table.Column<string>(type: "text", nullable: false),
                    companysecretcode = table.Column<string>(type: "text", nullable: true),
                    appkey = table.Column<string>(type: "text", nullable: true),
                    companycode = table.Column<string>(type: "text", nullable: true),
                    companyname = table.Column<string>(type: "text", nullable: true),
                    companynamebangla = table.Column<string>(type: "text", nullable: true),
                    companyshortname = table.Column<string>(type: "text", nullable: true),
                    primaryaddress = table.Column<string>(type: "text", nullable: true),
                    companyaddressbangla = table.Column<string>(type: "text", nullable: true),
                    secoundaryaddress = table.Column<string>(type: "text", nullable: true),
                    comphone = table.Column<string>(type: "text", nullable: true),
                    comphone2 = table.Column<string>(type: "text", nullable: true),
                    comfax = table.Column<string>(type: "text", nullable: true),
                    comemail = table.Column<string>(type: "text", nullable: true),
                    comweb = table.Column<string>(type: "text", nullable: true),
                    basecomid = table.Column<string>(type: "text", nullable: true),
                    countryid = table.Column<string>(type: "text", nullable: true),
                    decimalfield = table.Column<int>(type: "integer", nullable: true),
                    contperson = table.Column<string>(type: "text", nullable: true),
                    contdesig = table.Column<string>(type: "text", nullable: true),
                    isshowcurrencysymbol = table.Column<bool>(type: "boolean", nullable: true),
                    isinactive = table.Column<bool>(type: "boolean", nullable: true),
                    isgroup = table.Column<bool>(type: "boolean", nullable: true),
                    isdoller = table.Column<bool>(type: "boolean", nullable: true),
                    isbarcode = table.Column<bool>(type: "boolean", nullable: true),
                    isproduct = table.Column<bool>(type: "boolean", nullable: true),
                    iscorporate = table.Column<bool>(type: "boolean", nullable: true),
                    isposprint = table.Column<bool>(type: "boolean", nullable: true),
                    isservice = table.Column<bool>(type: "boolean", nullable: true),
                    isolddue = table.Column<bool>(type: "boolean", nullable: true),
                    isshortcutsale = table.Column<bool>(type: "boolean", nullable: true),
                    isrestaurantsale = table.Column<bool>(type: "boolean", nullable: true),
                    istouch = table.Column<bool>(type: "boolean", nullable: true),
                    isshoesale = table.Column<bool>(type: "boolean", nullable: true),
                    isimeisale = table.Column<bool>(type: "boolean", nullable: true),
                    ismultiplewh = table.Column<bool>(type: "boolean", nullable: true),
                    ismulticurrency = table.Column<bool>(type: "boolean", nullable: true),
                    ismultidebitcredit = table.Column<bool>(type: "boolean", nullable: true),
                    isvoucherdistributionentry = table.Column<bool>(type: "boolean", nullable: true),
                    ischequedetails = table.Column<bool>(type: "boolean", nullable: true),
                    comimageheader = table.Column<byte[]>(type: "bytea", nullable: true),
                    headerimagepath = table.Column<string>(type: "text", nullable: true),
                    headerfileextension = table.Column<string>(type: "text", nullable: true),
                    comlogo = table.Column<byte[]>(type: "bytea", nullable: true),
                    logoimagepath = table.Column<string>(type: "text", nullable: true),
                    logofileextension = table.Column<string>(type: "text", nullable: true),
                    comsign = table.Column<byte[]>(type: "bytea", nullable: true),
                    signimagepath = table.Column<string>(type: "text", nullable: true),
                    signfileextension = table.Column<string>(type: "text", nullable: true),
                    addvertise = table.Column<string>(type: "text", nullable: true),
                    isepz = table.Column<bool>(type: "boolean", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_companys", x => x.comid);
                    table.ForeignKey(
                        name: "fk_companys_countries_countryid",
                        column: x => x.countryid,
                        principalTable: "countries",
                        principalColumn: "countryid");
                });

            migrationBuilder.CreateTable(
                name: "states",
                columns: table => new
                {
                    stateid = table.Column<string>(type: "text", nullable: false),
                    statecode = table.Column<string>(type: "text", nullable: false),
                    countryid = table.Column<string>(type: "text", nullable: false),
                    statename = table.Column<string>(type: "text", nullable: false),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_states", x => x.stateid);
                    table.ForeignKey(
                        name: "fk_states_countries_countryid",
                        column: x => x.countryid,
                        principalTable: "countries",
                        principalColumn: "countryid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    deptcode = table.Column<string>(type: "text", nullable: false),
                    deptname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    deptbangla = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    slno = table.Column<short>(type: "smallint", nullable: true),
                    pcname = table.Column<string>(type: "text", nullable: false),
                    dtinput = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    depthodid = table.Column<int>(type: "integer", nullable: true),
                    deptclevelid = table.Column<int>(type: "integer", nullable: true),
                    isactualothide = table.Column<bool>(type: "boolean", nullable: false),
                    isbuyerothide = table.Column<bool>(type: "boolean", nullable: false),
                    isactualsalaryhide = table.Column<bool>(type: "boolean", nullable: false),
                    isbuyersalaryhide = table.Column<bool>(type: "boolean", nullable: false),
                    isbuyerotdiffer = table.Column<bool>(type: "boolean", nullable: false),
                    companyid = table.Column<string>(type: "text", nullable: true),
                    serial = table.Column<int>(type: "integer", nullable: true),
                    userid = table.Column<string>(type: "text", nullable: true),
                    updatebyuserid = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departments", x => x.id);
                    table.ForeignKey(
                        name: "fk_departments_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                });

            migrationBuilder.CreateTable(
                name: "designations",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    designame = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    designameb = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    salaryrange = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    slno = table.Column<int>(type: "integer", nullable: true),
                    gsmin = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    attbonus = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    holidaybonus = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    nightallow = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ttlmanpower = table.Column<int>(type: "integer", nullable: true),
                    proposedmanpower = table.Column<int>(type: "integer", nullable: true),
                    pcname = table.Column<string>(type: "text", nullable: true),
                    dtinput = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    companyid = table.Column<string>(type: "text", nullable: true),
                    serial = table.Column<int>(type: "integer", nullable: true),
                    userid = table.Column<string>(type: "text", nullable: true),
                    updatebyuserid = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designations", x => x.id);
                    table.ForeignKey(
                        name: "fk_designations_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                });

            migrationBuilder.CreateTable(
                name: "recruitmentvariables",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    defaulttype = table.Column<int>(type: "integer", nullable: true),
                    companyid = table.Column<string>(type: "text", nullable: true),
                    serial = table.Column<int>(type: "integer", nullable: true),
                    userid = table.Column<string>(type: "text", nullable: true),
                    updatebyuserid = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recruitmentvariables", x => x.id);
                    table.ForeignKey(
                        name: "fk_recruitmentvariables_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                });

            migrationBuilder.CreateTable(
                name: "citys",
                columns: table => new
                {
                    cityid = table.Column<string>(type: "text", nullable: false),
                    citycode = table.Column<string>(type: "text", nullable: false),
                    stateid = table.Column<string>(type: "text", nullable: false),
                    cityname = table.Column<string>(type: "text", nullable: false),
                    countryid = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_citys", x => x.cityid);
                    table.ForeignKey(
                        name: "fk_citys_countries_countryid",
                        column: x => x.countryid,
                        principalTable: "countries",
                        principalColumn: "countryid");
                    table.ForeignKey(
                        name: "fk_citys_states_stateid",
                        column: x => x.stateid,
                        principalTable: "states",
                        principalColumn: "stateid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "jobdescriptiontemplates",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    designation_id = table.Column<string>(type: "text", nullable: true),
                    designationid = table.Column<string>(type: "text", nullable: true),
                    job_title = table.Column<string>(type: "text", nullable: true),
                    skills = table.Column<string[]>(type: "text[]", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    responsibility = table.Column<string[]>(type: "text[]", nullable: true),
                    qulifications = table.Column<string[]>(type: "text[]", nullable: true),
                    benefits = table.Column<string[]>(type: "text[]", nullable: true),
                    otherinfo = table.Column<string[]>(type: "text[]", nullable: true),
                    defaulttype = table.Column<int>(type: "integer", nullable: true),
                    companyid = table.Column<string>(type: "text", nullable: true),
                    serial = table.Column<int>(type: "integer", nullable: true),
                    userid = table.Column<string>(type: "text", nullable: true),
                    updatebyuserid = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jobdescriptiontemplates", x => x.id);
                    table.ForeignKey(
                        name: "fk_jobdescriptiontemplates_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                    table.ForeignKey(
                        name: "fk_jobdescriptiontemplates_designations_designationid",
                        column: x => x.designationid,
                        principalTable: "designations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "jobposts",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    jobtypes = table.Column<string[]>(type: "text[]", nullable: true),
                    jobtags = table.Column<string[]>(type: "text[]", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    lastdate = table.Column<DateOnly>(type: "date", nullable: true),
                    salarymin = table.Column<string>(type: "text", nullable: true),
                    salarymax = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    responsibilities = table.Column<string[]>(type: "text[]", nullable: true),
                    qualifications = table.Column<string[]>(type: "text[]", nullable: true),
                    benefits = table.Column<string[]>(type: "text[]", nullable: true),
                    otherinformation = table.Column<string[]>(type: "text[]", nullable: true),
                    startdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    publishdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    skills = table.Column<string[]>(type: "text[]", nullable: true),
                    iscompleted = table.Column<string>(type: "text", nullable: true),
                    desination_id = table.Column<string>(type: "text", nullable: true),
                    designationid = table.Column<string>(type: "text", nullable: true),
                    department_id = table.Column<string>(type: "text", nullable: true),
                    companyid = table.Column<string>(type: "text", nullable: true),
                    serial = table.Column<int>(type: "integer", nullable: true),
                    userid = table.Column<string>(type: "text", nullable: true),
                    updatebyuserid = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jobposts", x => x.id);
                    table.ForeignKey(
                        name: "fk_jobposts_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                    table.ForeignKey(
                        name: "fk_jobposts_designations_designationid",
                        column: x => x.designationid,
                        principalTable: "designations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<int>(type: "integer", nullable: false),
                    image = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    departmentid = table.Column<string>(type: "text", nullable: true),
                    departmentid1 = table.Column<string>(type: "text", nullable: false),
                    designationid = table.Column<string>(type: "text", nullable: true),
                    designationid1 = table.Column<string>(type: "text", nullable: true),
                    companyid = table.Column<string>(type: "text", nullable: true),
                    serial = table.Column<int>(type: "integer", nullable: true),
                    userid = table.Column<string>(type: "text", nullable: true),
                    updatebyuserid = table.Column<string>(type: "text", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                    table.ForeignKey(
                        name: "fk_users_departments_departmentid1",
                        column: x => x.departmentid1,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_users_designations_designationid1",
                        column: x => x.designationid1,
                        principalTable: "designations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "milestones",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    jobdescription_id = table.Column<string>(type: "text", nullable: true),
                    defaulttype = table.Column<int>(type: "integer", nullable: true),
                    isassignment = table.Column<bool>(type: "boolean", nullable: true),
                    companyid = table.Column<string>(type: "text", nullable: true),
                    serial = table.Column<int>(type: "integer", nullable: true),
                    userid = table.Column<string>(type: "text", nullable: true),
                    updatebyuserid = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_milestones", x => x.id);
                    table.ForeignKey(
                        name: "fk_milestones_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                    table.ForeignKey(
                        name: "fk_milestones_jobdescriptiontemplates_jobdescription_id",
                        column: x => x.jobdescription_id,
                        principalTable: "jobdescriptiontemplates",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "jobmilestones",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    jobpost_id = table.Column<string>(type: "text", nullable: true),
                    jobpostsid = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    defaulttype = table.Column<int>(type: "integer", nullable: true),
                    isassignment = table.Column<bool>(type: "boolean", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true),
                    companyid = table.Column<string>(type: "text", nullable: true),
                    serial = table.Column<int>(type: "integer", nullable: true),
                    userid = table.Column<string>(type: "text", nullable: true),
                    updatebyuserid = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jobmilestones", x => x.id);
                    table.ForeignKey(
                        name: "fk_jobmilestones_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                    table.ForeignKey(
                        name: "fk_jobmilestones_jobposts_jobpostsid",
                        column: x => x.jobpostsid,
                        principalTable: "jobposts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "applicantapplicationstatus",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    applicant_id = table.Column<string>(type: "text", nullable: true),
                    milestone_id = table.Column<string>(type: "text", nullable: false),
                    jobpost_id = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_applicantapplicationstatus", x => x.id);
                    table.ForeignKey(
                        name: "fk_applicantapplicationstatus_jobmilestones_milestone_id",
                        column: x => x.milestone_id,
                        principalTable: "jobmilestones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_applicantapplicationstatus_jobposts_jobpost_id",
                        column: x => x.jobpost_id,
                        principalTable: "jobposts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "applicants",
                columns: table => new
                {
                    applicantid = table.Column<string>(type: "text", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: true),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    fathername = table.Column<string>(type: "text", nullable: true),
                    mothername = table.Column<string>(type: "text", nullable: true),
                    dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    religion = table.Column<string>(type: "text", nullable: true),
                    maritalstatus = table.Column<string>(type: "text", nullable: true),
                    nationality = table.Column<string>(type: "text", nullable: true),
                    nid = table.Column<string>(type: "text", nullable: true),
                    passportnumber = table.Column<string>(type: "text", nullable: true),
                    passportissuedate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    primarymno = table.Column<string>(type: "text", nullable: true),
                    secmno = table.Column<string>(type: "text", nullable: true),
                    emergencycontact = table.Column<string>(type: "text", nullable: true),
                    primaryemail = table.Column<string>(type: "text", nullable: true),
                    passportnumberwithoutunderscores = table.Column<string>(type: "text", nullable: true),
                    jobapplicationid = table.Column<string>(type: "text", nullable: true),
                    jobpostid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_applicants", x => x.applicantid);
                    table.ForeignKey(
                        name: "fk_applicants_jobposts_jobpostid",
                        column: x => x.jobpostid,
                        principalTable: "jobposts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "assignments",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    notice = table.Column<string>(type: "text", nullable: true),
                    duedate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    applicantid = table.Column<string>(type: "text", nullable: true),
                    jobpostid = table.Column<string>(type: "text", nullable: true),
                    companyid = table.Column<string>(type: "text", nullable: true),
                    serial = table.Column<int>(type: "integer", nullable: true),
                    userid = table.Column<string>(type: "text", nullable: true),
                    updatebyuserid = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignments", x => x.id);
                    table.ForeignKey(
                        name: "fk_assignments_applicants_applicantid",
                        column: x => x.applicantid,
                        principalTable: "applicants",
                        principalColumn: "applicantid");
                    table.ForeignKey(
                        name: "fk_assignments_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                    table.ForeignKey(
                        name: "fk_assignments_jobposts_jobpostid",
                        column: x => x.jobpostid,
                        principalTable: "jobposts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "jobapplications",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    jobpostid = table.Column<string>(type: "text", nullable: true),
                    applicantid = table.Column<string>(type: "text", nullable: true),
                    companyid = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    university = table.Column<string>(type: "text", nullable: true),
                    phonenumber = table.Column<string>(type: "text", nullable: true),
                    skills = table.Column<string>(type: "jsonb", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    currentlocation = table.Column<string>(type: "text", nullable: true),
                    applyingposition = table.Column<string>(type: "text", nullable: true),
                    experience = table.Column<string>(type: "text", nullable: true),
                    linkedinprofilelink = table.Column<string>(type: "text", nullable: true),
                    previousjobcompanyname = table.Column<string>(type: "text", nullable: true),
                    currentsalary = table.Column<string>(type: "text", nullable: true),
                    expectedsalary = table.Column<string>(type: "text", nullable: true),
                    coverletter = table.Column<string>(type: "text", nullable: true),
                    howdidyouknow = table.Column<string>(type: "text", nullable: true),
                    resumeurl = table.Column<string>(type: "text", nullable: true),
                    govtidurl = table.Column<string>(type: "text", nullable: true),
                    certificateurl = table.Column<string>(type: "text", nullable: true),
                    transcripturl = table.Column<string>(type: "text", nullable: true),
                    ssccertificateurl = table.Column<string>(type: "text", nullable: true),
                    msccertificateurl = table.Column<string>(type: "text", nullable: true),
                    bsccertificateurl = table.Column<string>(type: "text", nullable: true),
                    dateapplied = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jobapplications", x => x.id);
                    table.ForeignKey(
                        name: "fk_jobapplications_applicants_applicantid",
                        column: x => x.applicantid,
                        principalTable: "applicants",
                        principalColumn: "applicantid");
                    table.ForeignKey(
                        name: "fk_jobapplications_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                    table.ForeignKey(
                        name: "fk_jobapplications_jobposts_jobpostid",
                        column: x => x.jobpostid,
                        principalTable: "jobposts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "resumes",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    applicantid = table.Column<string>(type: "text", nullable: false),
                    fullname = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    summary = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_resumes", x => x.id);
                    table.ForeignKey(
                        name: "fk_resumes_applicants_applicantid",
                        column: x => x.applicantid,
                        principalTable: "applicants",
                        principalColumn: "applicantid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "applicantsassignments",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    applicantid = table.Column<string>(type: "text", nullable: false),
                    assignmentid = table.Column<string>(type: "text", nullable: false),
                    uploadedfilepath = table.Column<string>(type: "text", nullable: true),
                    issubmitted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_applicantsassignments", x => x.id);
                    table.ForeignKey(
                        name: "fk_applicantsassignments_applicants_applicantid",
                        column: x => x.applicantid,
                        principalTable: "applicants",
                        principalColumn: "applicantid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_applicantsassignments_assignments_assignmentid",
                        column: x => x.assignmentid,
                        principalTable: "assignments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "certifications",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    resumeid = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    issuingorganization = table.Column<string>(type: "text", nullable: false),
                    dateissued = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dateexpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_certifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_certifications_resumes_resumeid",
                        column: x => x.resumeid,
                        principalTable: "resumes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "educations",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    resumeid = table.Column<string>(type: "text", nullable: false),
                    institution = table.Column<string>(type: "text", nullable: false),
                    degree = table.Column<string>(type: "text", nullable: false),
                    fieldofstudy = table.Column<string>(type: "text", nullable: false),
                    startdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    enddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_educations", x => x.id);
                    table.ForeignKey(
                        name: "fk_educations_resumes_resumeid",
                        column: x => x.resumeid,
                        principalTable: "resumes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    resumeid = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    startdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    enddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.id);
                    table.ForeignKey(
                        name: "fk_projects_resumes_resumeid",
                        column: x => x.resumeid,
                        principalTable: "resumes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    resumeid = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    proficiency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_skills", x => x.id);
                    table.ForeignKey(
                        name: "fk_skills_resumes_resumeid",
                        column: x => x.resumeid,
                        principalTable: "resumes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workexperiences",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    resumeid = table.Column<string>(type: "text", nullable: false),
                    company = table.Column<string>(type: "text", nullable: false),
                    position = table.Column<string>(type: "text", nullable: false),
                    startdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    enddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workexperiences", x => x.id);
                    table.ForeignKey(
                        name: "fk_workexperiences_resumes_resumeid",
                        column: x => x.resumeid,
                        principalTable: "resumes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_applicantapplicationstatus_applicant_id",
                table: "applicantapplicationstatus",
                column: "applicant_id");

            migrationBuilder.CreateIndex(
                name: "ix_applicantapplicationstatus_jobpost_id",
                table: "applicantapplicationstatus",
                column: "jobpost_id");

            migrationBuilder.CreateIndex(
                name: "ix_applicantapplicationstatus_milestone_id",
                table: "applicantapplicationstatus",
                column: "milestone_id");

            migrationBuilder.CreateIndex(
                name: "ix_applicants_jobapplicationid",
                table: "applicants",
                column: "jobapplicationid");

            migrationBuilder.CreateIndex(
                name: "ix_applicants_jobpostid",
                table: "applicants",
                column: "jobpostid");

            migrationBuilder.CreateIndex(
                name: "ix_applicantsassignments_applicantid",
                table: "applicantsassignments",
                column: "applicantid");

            migrationBuilder.CreateIndex(
                name: "ix_applicantsassignments_assignmentid",
                table: "applicantsassignments",
                column: "assignmentid");

            migrationBuilder.CreateIndex(
                name: "ix_assignments_applicantid",
                table: "assignments",
                column: "applicantid");

            migrationBuilder.CreateIndex(
                name: "ix_assignments_companyid",
                table: "assignments",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_assignments_jobpostid",
                table: "assignments",
                column: "jobpostid");

            migrationBuilder.CreateIndex(
                name: "ix_certifications_resumeid",
                table: "certifications",
                column: "resumeid");

            migrationBuilder.CreateIndex(
                name: "ix_citys_countryid",
                table: "citys",
                column: "countryid");

            migrationBuilder.CreateIndex(
                name: "ix_citys_stateid",
                table: "citys",
                column: "stateid");

            migrationBuilder.CreateIndex(
                name: "ix_companys_countryid",
                table: "companys",
                column: "countryid");

            migrationBuilder.CreateIndex(
                name: "ix_departments_companyid",
                table: "departments",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_designations_companyid",
                table: "designations",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_educations_resumeid",
                table: "educations",
                column: "resumeid");

            migrationBuilder.CreateIndex(
                name: "ix_jobapplications_applicantid",
                table: "jobapplications",
                column: "applicantid");

            migrationBuilder.CreateIndex(
                name: "ix_jobapplications_companyid",
                table: "jobapplications",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_jobapplications_jobpostid",
                table: "jobapplications",
                column: "jobpostid");

            migrationBuilder.CreateIndex(
                name: "ix_jobdescriptiontemplates_companyid",
                table: "jobdescriptiontemplates",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_jobdescriptiontemplates_designationid",
                table: "jobdescriptiontemplates",
                column: "designationid");

            migrationBuilder.CreateIndex(
                name: "ix_jobmilestones_companyid",
                table: "jobmilestones",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_jobmilestones_jobpostsid",
                table: "jobmilestones",
                column: "jobpostsid");

            migrationBuilder.CreateIndex(
                name: "ix_jobposts_companyid",
                table: "jobposts",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_jobposts_designationid",
                table: "jobposts",
                column: "designationid");

            migrationBuilder.CreateIndex(
                name: "ix_milestones_companyid",
                table: "milestones",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_milestones_jobdescription_id",
                table: "milestones",
                column: "jobdescription_id");

            migrationBuilder.CreateIndex(
                name: "ix_projects_resumeid",
                table: "projects",
                column: "resumeid");

            migrationBuilder.CreateIndex(
                name: "ix_recruitmentvariables_companyid",
                table: "recruitmentvariables",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_resumes_applicantid",
                table: "resumes",
                column: "applicantid");

            migrationBuilder.CreateIndex(
                name: "ix_skills_resumeid",
                table: "skills",
                column: "resumeid");

            migrationBuilder.CreateIndex(
                name: "ix_states_countryid",
                table: "states",
                column: "countryid");

            migrationBuilder.CreateIndex(
                name: "ix_users_companyid",
                table: "users",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_users_departmentid1",
                table: "users",
                column: "departmentid1");

            migrationBuilder.CreateIndex(
                name: "ix_users_designationid1",
                table: "users",
                column: "designationid1");

            migrationBuilder.CreateIndex(
                name: "ix_workexperiences_resumeid",
                table: "workexperiences",
                column: "resumeid");

            migrationBuilder.AddForeignKey(
                name: "fk_applicantapplicationstatus_applicants_applicant_id",
                table: "applicantapplicationstatus",
                column: "applicant_id",
                principalTable: "applicants",
                principalColumn: "applicantid");

            migrationBuilder.AddForeignKey(
                name: "fk_applicants_jobapplications_jobapplicationid",
                table: "applicants",
                column: "jobapplicationid",
                principalTable: "jobapplications",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_jobapplications_applicants_applicantid",
                table: "jobapplications");

            migrationBuilder.DropTable(
                name: "applicantapplicationstatus");

            migrationBuilder.DropTable(
                name: "applicantsassignments");

            migrationBuilder.DropTable(
                name: "certifications");

            migrationBuilder.DropTable(
                name: "citys");

            migrationBuilder.DropTable(
                name: "educations");

            migrationBuilder.DropTable(
                name: "milestones");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "recruitmentvariables");

            migrationBuilder.DropTable(
                name: "skills");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "workexperiences");

            migrationBuilder.DropTable(
                name: "jobmilestones");

            migrationBuilder.DropTable(
                name: "assignments");

            migrationBuilder.DropTable(
                name: "states");

            migrationBuilder.DropTable(
                name: "jobdescriptiontemplates");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "resumes");

            migrationBuilder.DropTable(
                name: "applicants");

            migrationBuilder.DropTable(
                name: "jobapplications");

            migrationBuilder.DropTable(
                name: "jobposts");

            migrationBuilder.DropTable(
                name: "designations");

            migrationBuilder.DropTable(
                name: "companys");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
