using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.API.Migrations
{
    public partial class InitialAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Alpha2 = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Alpha3 = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    NumericCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Alpha2", "Alpha3", "Name", "NumericCode" },
                values: new object[,]
                {
                    { 1, "AF", "AFG", "Afghanistan", "0004" },
                    { 2, "AL", "ALB", "Albania", "0008" },
                    { 3, "DZ", "DZA", "Algeria", "0012" },
                    { 4, "AS", "ASM", "American Samoa", "0016" },
                    { 5, "AD", "AND", "Andorra", "0020" },
                    { 6, "AO", "AGO", "Angola", "0024" },
                    { 7, "AI", "AIA", "Anguilla", "0660" },
                    { 8, "AQ", "ATA", "Antarctica", "0010" },
                    { 9, "AG", "ATG", "Antigua and Barbuda", "0028" },
                    { 10, "AR", "ARG", "Argentina", "0032" },
                    { 11, "AM", "ARM", "Armenia", "0051" },
                    { 12, "AW", "ABW", "Aruba", "0533" },
                    { 13, "AU", "AUS", "Australia", "0036" },
                    { 14, "AT", "AUT", "Austria", "0040" },
                    { 15, "AZ", "AZE", "Azerbaijan", "0031" },
                    { 16, "BS", "BHS", "Bahamas (the)", "0044" },
                    { 17, "BH", "BHR", "Bahrain", "0048" },
                    { 18, "BD", "BGD", "Bangladesh", "0050" },
                    { 19, "BB", "BRB", "Barbados", "0052" },
                    { 20, "BY", "BLR", "Belarus", "0112" },
                    { 21, "BE", "BEL", "Belgium", "0056" },
                    { 22, "BZ", "BLZ", "Belize", "0084" },
                    { 23, "BJ", "BEN", "Benin", "0204" },
                    { 24, "BM", "BMU", "Bermuda", "0060" },
                    { 25, "BT", "BTN", "Bhutan", "0064" },
                    { 26, "BO", "BOL", "Bolivia (Plurinational State of)", "0068" },
                    { 27, "BQ", "BES", "Bonaire, Sint Eustatius and Saba", "0535" },
                    { 28, "BA", "BIH", "Bosnia and Herzegovina", "0070" },
                    { 29, "BW", "BWA", "Botswana", "0072" },
                    { 30, "BV", "BVT", "Bouvet Island", "0074" },
                    { 31, "BR", "BRA", "Brazil", "0076" },
                    { 32, "IO", "IOT", "British Indian Ocean Territory (the)", "0086" },
                    { 33, "BN", "BRN", "Brunei Darussalam", "0096" },
                    { 34, "BG", "BGR", "Bulgaria", "0100" },
                    { 35, "BF", "BFA", "Burkina Faso", "0854" },
                    { 36, "BI", "BDI", "Burundi", "0108" },
                    { 37, "CV", "CPV", "Cabo Verde", "0132" },
                    { 38, "KH", "KHM", "Cambodia", "0116" },
                    { 39, "CM", "CMR", "Cameroon", "0120" },
                    { 40, "CA", "CAN", "Canada", "0124" },
                    { 41, "KY", "CYM", "Cayman Islands (the)", "0136" },
                    { 42, "CF", "CAF", "Central African Republic (the)", "0140" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Alpha2", "Alpha3", "Name", "NumericCode" },
                values: new object[,]
                {
                    { 43, "TD", "TCD", "Chad", "0148" },
                    { 44, "CL", "CHL", "Chile", "0152" },
                    { 45, "CN", "CHN", "China", "0156" },
                    { 46, "CX", "CXR", "Christmas Island", "0162" },
                    { 47, "CC", "CCK", "Cocos (Keeling) Islands (the)", "0166" },
                    { 48, "CO", "COL", "Colombia", "0170" },
                    { 49, "KM", "COM", "Comoros (the)", "0174" },
                    { 50, "CD", "COD", "Congo (the Democratic Republic of the)", "0180" },
                    { 51, "CG", "COG", "Congo (the)", "0178" },
                    { 52, "CK", "COK", "Cook Islands (the)", "0184" },
                    { 53, "CR", "CRI", "Costa Rica", "0188" },
                    { 54, "HR", "HRV", "Croatia", "0191" },
                    { 55, "CU", "CUB", "Cuba", "0192" },
                    { 56, "CW", "CUW", "Curaçao", "0531" },
                    { 57, "CY", "CYP", "Cyprus", "0196" },
                    { 58, "CZ", "CZE", "Czechia", "0203" },
                    { 59, "CI", "CIV", "Côte d'Ivoire", "0384" },
                    { 60, "DK", "DNK", "Denmark", "0208" },
                    { 61, "DJ", "DJI", "Djibouti", "0262" },
                    { 62, "DM", "DMA", "Dominica", "0212" },
                    { 63, "DO", "DOM", "Dominican Republic (the)", "0214" },
                    { 64, "EC", "ECU", "Ecuador", "0218" },
                    { 65, "EG", "EGY", "Egypt", "0818" },
                    { 66, "SV", "SLV", "El Salvador", "0222" },
                    { 67, "GQ", "GNQ", "Equatorial Guinea", "0226" },
                    { 68, "ER", "ERI", "Eritrea", "0232" },
                    { 69, "EE", "EST", "Estonia", "0233" },
                    { 70, "SZ", "SWZ", "Eswatini", "0748" },
                    { 71, "ET", "ETH", "Ethiopia", "0231" },
                    { 72, "FK", "FLK", "Falkland Islands (the) [Malvinas]", "0238" },
                    { 73, "FO", "FRO", "Faroe Islands (the)", "0234" },
                    { 74, "FJ", "FJI", "Fiji", "0242" },
                    { 75, "FI", "FIN", "Finland", "0246" },
                    { 76, "FR", "FRA", "France", "0250" },
                    { 77, "GF", "GUF", "French Guiana", "0254" },
                    { 78, "PF", "PYF", "French Polynesia", "0258" },
                    { 79, "TF", "ATF", "French Southern Territories (the)", "0260" },
                    { 80, "GA", "GAB", "Gabon", "0266" },
                    { 81, "GM", "GMB", "Gambia (the)", "0270" },
                    { 82, "GE", "GEO", "Georgia", "0268" },
                    { 83, "DE", "DEU", "Germany", "0276" },
                    { 84, "GH", "GHA", "Ghana", "0288" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Alpha2", "Alpha3", "Name", "NumericCode" },
                values: new object[,]
                {
                    { 85, "GI", "GIB", "Gibraltar", "0292" },
                    { 86, "GR", "GRC", "Greece", "0300" },
                    { 87, "GL", "GRL", "Greenland", "0304" },
                    { 88, "GD", "GRD", "Grenada", "0308" },
                    { 89, "GP", "GLP", "Guadeloupe", "0312" },
                    { 90, "GU", "GUM", "Guam", "0316" },
                    { 91, "GT", "GTM", "Guatemala", "0320" },
                    { 92, "GG", "GGY", "Guernsey", "0831" },
                    { 93, "GN", "GIN", "Guinea", "0324" },
                    { 94, "GW", "GNB", "Guinea-Bissau", "0624" },
                    { 95, "GY", "GUY", "Guyana", "0328" },
                    { 96, "HT", "HTI", "Haiti", "0332" },
                    { 97, "HM", "HMD", "Heard Island and McDonald Islands", "0334" },
                    { 98, "VA", "VAT", "Holy See (the)", "0336" },
                    { 99, "HN", "HND", "Honduras", "0340" },
                    { 100, "HK", "HKG", "Hong Kong", "0344" },
                    { 101, "HU", "HUN", "Hungary", "0348" },
                    { 102, "IS", "ISL", "Iceland", "0352" },
                    { 103, "IN", "IND", "India", "0356" },
                    { 104, "ID", "IDN", "Indonesia", "0360" },
                    { 105, "IR", "IRN", "Iran (Islamic Republic of)", "0364" },
                    { 106, "IQ", "IRQ", "Iraq", "0368" },
                    { 107, "IE", "IRL", "Ireland", "0372" },
                    { 108, "IM", "IMN", "Isle of Man", "0833" },
                    { 109, "IL", "ISR", "Israel", "0376" },
                    { 110, "IT", "ITA", "Italy", "0380" },
                    { 111, "JM", "JAM", "Jamaica", "0388" },
                    { 112, "JP", "JPN", "Japan", "0392" },
                    { 113, "JE", "JEY", "Jersey", "0832" },
                    { 114, "JO", "JOR", "Jordan", "0400" },
                    { 115, "KZ", "KAZ", "Kazakhstan", "0398" },
                    { 116, "KE", "KEN", "Kenya", "0404" },
                    { 117, "KI", "KIR", "Kiribati", "0296" },
                    { 118, "KP", "PRK", "Korea (the Democratic People's Republic of)", "0408" },
                    { 119, "KR", "KOR", "Korea (the Republic of)", "0410" },
                    { 120, "KW", "KWT", "Kuwait", "0414" },
                    { 121, "KG", "KGZ", "Kyrgyzstan", "0417" },
                    { 122, "LA", "LAO", "Lao People's Democratic Republic(the)", "0418" },
                    { 123, "LV", "LVA", "Latvia", "0428" },
                    { 124, "LB", "LBN", "Lebanon", "0422" },
                    { 125, "LS", "LSO", "Lesotho", "0426" },
                    { 126, "LR", "LBR", "Liberia", "0430" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Alpha2", "Alpha3", "Name", "NumericCode" },
                values: new object[,]
                {
                    { 127, "LY", "LBY", "Libya", "0434" },
                    { 128, "LI", "LIE", "Liechtenstein", "0438" },
                    { 129, "LT", "LTU", "Lithuania", "0440" },
                    { 130, "LU", "LUX", "Luxembourg", "0442" },
                    { 131, "MO", "MAC", "Macao", "0446" },
                    { 132, "MG", "MDG", "Madagascar", "0450" },
                    { 133, "MW", "MWI", "Malawi", "0454" },
                    { 134, "MY", "MYS", "Malaysia", "0458" },
                    { 135, "MV", "MDV", "Maldives", "0462" },
                    { 136, "ML", "MLI", "Mali", "0466" },
                    { 137, "MT", "MLT", "Malta", "0470" },
                    { 138, "MH", "MHL", "Marshall Islands (the)", "0584" },
                    { 139, "MQ", "MTQ", "Martinique", "0474" },
                    { 140, "MR", "MRT", "Mauritania", "0478" },
                    { 141, "MU", "MUS", "Mauritius", "0480" },
                    { 142, "YT", "MYT", "Mayotte", "0175" },
                    { 143, "MX", "MEX", "Mexico", "0484" },
                    { 144, "FM", "FSM", "Micronesia (Federated States of)", "0583" },
                    { 145, "MD", "MDA", "Moldova (the Republic of)", "0498" },
                    { 146, "MC", "MCO", "Monaco", "0492" },
                    { 147, "MN", "MNG", "Mongolia", "0496" },
                    { 148, "ME", "MNE", "Montenegro", "0499" },
                    { 149, "MS", "MSR", "Montserrat", "0500" },
                    { 150, "MA", "MAR", "Morocco", "0504" },
                    { 151, "MZ", "MOZ", "Mozambique", "0508" },
                    { 152, "MM", "MMR", "Myanmar", "0104" },
                    { 153, "NA", "NAM", "Namibia", "0516" },
                    { 154, "NR", "NRU", "Nauru", "0520" },
                    { 155, "NP", "NPL", "Nepal", "0524" },
                    { 156, "NL", "NLD", "Netherlands (the)", "0528" },
                    { 157, "NC", "NCL", "New Caledonia", "0540" },
                    { 158, "NZ", "NZL", "New Zealand", "0554" },
                    { 159, "NI", "NIC", "Nicaragua", "0558" },
                    { 160, "NE", "NER", "Niger (the)", "0562" },
                    { 161, "NG", "NGA", "Nigeria", "0566" },
                    { 162, "NU", "NIU", "Niue", "0570" },
                    { 163, "NF", "NFK", "Norfolk Island", "0574" },
                    { 164, "MP", "MNP", "Northern Mariana Islands (the)", "0580" },
                    { 165, "NO", "NOR", "Norway", "0578" },
                    { 166, "OM", "OMN", "Oman", "0512" },
                    { 167, "PK", "PAK", "Pakistan", "0586" },
                    { 168, "PW", "PLW", "Palau", "0585" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Alpha2", "Alpha3", "Name", "NumericCode" },
                values: new object[,]
                {
                    { 169, "PS", "PSE", "Palestine, State of", "0275" },
                    { 170, "PA", "PAN", "Panama", "0591" },
                    { 171, "PG", "PNG", "Papua New Guinea", "0598" },
                    { 172, "PY", "PRY", "Paraguay", "0600" },
                    { 173, "PE", "PER", "Peru", "0604" },
                    { 174, "PH", "PHL", "Philippines (the)", "0608" },
                    { 175, "PN", "PCN", "Pitcairn", "0612" },
                    { 176, "PL", "POL", "Poland", "0616" },
                    { 177, "PT", "PRT", "Portugal", "0620" },
                    { 178, "PR", "PRI", "Puerto Rico", "0630" },
                    { 179, "QA", "QAT", "Qatar", "0634" },
                    { 180, "MK", "MKD", "Republic of North Macedonia", "0807" },
                    { 181, "RO", "ROU", "Romania", "0642" },
                    { 182, "RU", "RUS", "Russian Federation (the)", "0643" },
                    { 183, "RW", "RWA", "Rwanda", "0646" },
                    { 184, "RE", "REU", "Réunion", "0638" },
                    { 185, "BL", "BLM", "Saint Barthélemy", "0652" },
                    { 186, "SH", "SHN", "Saint Helena, Ascension and Tristan da Cunha", "0654" },
                    { 187, "KN", "KNA", "Saint Kitts and Nevis", "0659" },
                    { 188, "LC", "LCA", "Saint Lucia", "0662" },
                    { 189, "MF", "MAF", "Saint Martin (French part)", "0663" },
                    { 190, "PM", "SPM", "Saint Pierre and Miquelon", "0666" },
                    { 191, "VC", "VCT", "Saint Vincent and the Grenadines", "0670" },
                    { 192, "WS", "WSM", "Samoa", "0882" },
                    { 193, "SM", "SMR", "San Marino", "0674" },
                    { 194, "ST", "STP", "Sao Tome and Principe", "0678" },
                    { 195, "SA", "SAU", "Saudi Arabia", "0682" },
                    { 196, "SN", "SEN", "Senegal", "0686" },
                    { 197, "RS", "SRB", "Serbia", "0688" },
                    { 198, "SC", "SYC", "Seychelles", "0690" },
                    { 199, "SL", "SLE", "Sierra Leone", "0694" },
                    { 200, "SG", "SGP", "Singapore", "0702" },
                    { 201, "SX", "SXM", "Sint Maarten (Dutch part)", "0534" },
                    { 202, "SK", "SVK", "Slovakia", "0703" },
                    { 203, "SI", "SVN", "Slovenia", "0705" },
                    { 204, "SB", "SLB", "Solomon Islands", "0090" },
                    { 205, "SO", "SOM", "Somalia", "0706" },
                    { 206, "ZA", "ZAF", "South Africa", "0710" },
                    { 207, "GS", "SGS", "South Georgia and the South Sandwich Islands", "0239" },
                    { 208, "SS", "SSD", "South Sudan", "0728" },
                    { 209, "ES", "ESP", "Spain", "0724" },
                    { 210, "LK", "LKA", "Sri Lanka", "0144" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Alpha2", "Alpha3", "Name", "NumericCode" },
                values: new object[,]
                {
                    { 211, "SD", "SDN", "Sudan (the)", "0729" },
                    { 212, "SR", "SUR", "Suriname", "0740" },
                    { 213, "SJ", "SJM", "Svalbard and Jan Mayen", "0744" },
                    { 214, "SE", "SWE", "Sweden", "0752" },
                    { 215, "CH", "CHE", "Switzerland", "0756" },
                    { 216, "SY", "SYR", "Syrian Arab Republic", "0760" },
                    { 217, "TW", "TWN", "Taiwan (Province of China)", "0158" },
                    { 218, "TJ", "TJK", "Tajikistan", "0762" },
                    { 219, "TZ", "TZA", "Tanzania, United Republic of", "0834" },
                    { 220, "TH", "THA", "Thailand", "0764" },
                    { 221, "TL", "TLS", "Timor-Leste", "0626" },
                    { 222, "TG", "TGO", "Togo", "0768" },
                    { 223, "TK", "TKL", "Tokelau", "0772" },
                    { 224, "TO", "TON", "Tonga", "0776" },
                    { 225, "TT", "TTO", "Trinidad and Tobago", "0780" },
                    { 226, "TN", "TUN", "Tunisia", "0788" },
                    { 227, "TR", "TUR", "Turkey", "0792" },
                    { 228, "TM", "TKM", "Turkmenistan", "0795" },
                    { 229, "TC", "TCA", "Turks and Caicos Islands (the)", "0796" },
                    { 230, "TV", "TUV", "Tuvalu", "0798" },
                    { 231, "UG", "UGA", "Uganda", "0800" },
                    { 232, "UA", "UKR", "Ukraine", "0804" },
                    { 233, "AE", "ARE", "United Arab Emirates (the)", "0784" },
                    { 234, "GB", "GBR", "United Kingdom of Great Britain and Northern Ireland (the)", "0826" },
                    { 235, "UM", "UMI", "United States Minor Outlying Islands (the)", "0581" },
                    { 236, "US", "USA", "United States of America (the)", "0840" },
                    { 237, "UY", "URY", "Uruguay", "0858" },
                    { 238, "UZ", "UZB", "Uzbekistan", "0860" },
                    { 239, "VU", "VUT", "Vanuatu", "0548" },
                    { 240, "VE", "VEN", "Venezuela (Bolivarian Republic of)", "0862" },
                    { 241, "VN", "VNM", "Viet Nam", "0704" },
                    { 242, "VG", "VGB", "Virgin Islands (British)", "0092" },
                    { 243, "VI", "VIR", "Virgin Islands (U.S.)", "0850" },
                    { 244, "WF", "WLF", "Wallis and Futuna", "0876" },
                    { 245, "EH", "ESH", "Western Sahara", "0732" },
                    { 246, "YE", "YEM", "Yemen", "0887" },
                    { 247, "ZM", "ZMB", "Zambia", "0894" },
                    { 248, "ZW", "ZWE", "Zimbabwe", "0716" },
                    { 249, "AX", "ALA", "Åland Islands", "0248" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "CountryId", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, "James St", 19, "Komodo Bay", 3.5 },
                    { 2, "Main St", 49, "Sun star", 3.5 },
                    { 3, "Lowell St", 135, "Lovitz Island", 4.0 },
                    { 4, "Maradona St", 135, "Lovell", 5.0 },
                    { 5, "Gran Via, 15", 209, "Ritz Madrid", 1.5 },
                    { 6, "Rambla, 12", 209, "Carlton Barcelona", 4.0 },
                    { 8, "Motherfucker St.", 236, "Trump Mar-a-Lago", 4.5 },
                    { 9, "Tontolaba St.", 240, "Maduro's Wonderland", 4.0 },
                    { 10, "Tarantino St.", 242, "Virgin Madonna", 3.5 },
                    { 76, "Carballeda, 32", 209, "Carlton Bilbao", 2.5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CountryId",
                table: "Hotels",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
