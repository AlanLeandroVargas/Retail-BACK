using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Retail.Migrations
{
    /// <inheritdoc />
    public partial class INITIALIZE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Taxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SaleId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_Category",
                        column: x => x.Category,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleProduct",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sale = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleProduct", x => x.ShoppingCartId);
                    table.ForeignKey(
                        name: "FK_SaleProduct_Product_Product",
                        column: x => x.Product,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleProduct_Sale_Sale",
                        column: x => x.Sale,
                        principalTable: "Sale",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Electrodomésticos" },
                    { 2, "Tecnología y Electrónica" },
                    { 3, "Moda y Accesorios" },
                    { 4, "Hogar y Decoración" },
                    { 5, "Salud y Belleza" },
                    { 6, "Deportes y Ocio" },
                    { 7, "Juguetes y Juegos" },
                    { 8, "Alimentos y Bebidas" },
                    { 9, "Libros y Material Educativo" },
                    { 10, "Jardinería y Bricolaje" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Category", "Description", "Discount", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("009ac5d4-9470-4edc-aad2-edd8550af1a9"), 4, "En ALA quisimos dar el próximo paso con el nuevo ALA líquido concentrado para diluir con Bicarbonato. La nueva fórmula del jabón líquido concentrado para diluir ALA Ecolavado con Bicarbonato tiene un poder de limpieza superior y agentes de limpieza naturales que remueven las manchas más difíciles, en el primer lavado. Este jabón para diluir combate el mal olor y es ideal para ropa blanca y de color. Además de tener una fragancia duradera. Por otro lado, su fórmula, con una tecnología patentada que se potencia en contacto con el agua y el agregado del bicarbonato que contribuye a mantener la blancura de las prendas, hace de este jabón para ropa un producto de limpieza profunda y superior. Este jabón concentrado rinde 3 L dado que su formato es para diluir. Para prepararlo, solo tenés que llenar una botella vacía de ALA 3 L con 2,5?L de agua potable, agregar el ALA líquido para diluir, mezclar 5 veces de arriba hacia abajo - ¡Y listo, ya tenés tu jabón líquido preparado con la misma consistencia y fragancia riquísima de siempre! Se dosifican 100?ml para una carga de ropa completa o 150?ml para ropa muy sucia. ALA, porque ensuciarse hace bien. Ingresá a www.ala.com.ar y conocé más de nuestros productos.", 30, "https://ardiaprod.vtexassets.com/arquivos/ids/286084-800-auto?v=638362510764470000&width=800&height=auto&aspect=true", "Jabón Líquido Concentrado para Diluir Ala con Bicarbonato 500 Ml.", 6340.00m },
                    { new Guid("02f5c6e8-ea4f-487f-b58d-4e0f2bde612f"), 2, "Celular libre", 30, "https://carrefourar.vtexassets.com/arquivos/ids/367471-1600-auto?v=638283212666130000&width=1600&height=auto&aspect=true", "Celular libre Motorola g23 4gb 128gb blanco", 399999.00m },
                    { new Guid("04d0f93c-20ce-490d-812c-14d382ac02e0"), 1, "Tecnología Principal: Inverter Dimensiones del producto 84,6 x 59,5 x 64,4 cm Dimensiones con embalaje 87,7 x 67,6 x 69,8 cm Capacidad bruta 64 L / 9,5 KG Color Blanco Tipo (abertura de puerta) Frontal Puerta Vidrio/Plástico Ciclos (listar) 24 Ciclos: Rápido 45 Quita Manchas PRO Refrescar con Vapor Lana Edredón Eco EEE Enjuagar y Centrifugar Centrifugar Diario Color Blanca Rápido 15 Delicada Sanitizar Sábanas y toallas + Ciclos adicionales Filtro (sí/no) Si Sexto Sentido Si Origen Argentina Sistema de lavado Frontal Revoluciones por minuto 1400 RPM Peso 79,7 KG Pies Niveladores Si Ruedas No Display Si Control de temperatura Si Alarma de puerta Si Traba de seguridad Si Función Antiarrugas Si Tambor Acero inoxidable Garantía 12 meses + 10 años garantía especial limitada en el motor Tipo de adaptador Argentino Tipo de cable Estándard con enchufe Nivel de ruido N/A Consumo de agua 64 L Media carga Incluye sensado de carga Eficacia de Lavado A Eficacia del Centrifugado B Eficiencia Energética / Clase Climática A++ Bomba de desagote Sí Lavado a Mano Incluye ciclo de lavado delicado Exclusión de Centrifugado Si Agua fría / caliente Incluye calentador interno", 20, "https://ardiaprod.vtexassets.com/arquivos/ids/261242-800-auto?v=638313511734830000&width=800&height=auto&aspect=true", "Lavarropas Whirlpool 9,5kg - 1400rpm Blanco Inverter", 1549999.00m },
                    { new Guid("06028ae9-d7ac-4298-9641-76d1cc513b79"), 8, "LA SERENÍSIMA CLÁSICO, yogur bebible, parcialmente descremado, sabor frutilla.?De la mejor leche salen los mejores yogures, disfrutá un vaso de yogur todos los días. SACHET 900g.?Yogur con probióticos endulzado parcialmente descremado fortificado con zinc y vitaminas A y D sabor frutilla libre de gluten – licuado. Sin TACC.", 25, "https://ardiaprod.vtexassets.com/arquivos/ids/287581-800-auto?v=638383484383700000&width=800&height=auto&aspect=true", "Yogur Bebible Frutilla La Serenísima Clásico 900 Gr.", 2095.00m },
                    { new Guid("07b8360c-d2ef-42bc-9625-f6518dca1e70"), 3, "Rompeviento", 30, "https://carrefourar.vtexassets.com/arquivos/ids/496975-800-auto?v=638476855667000000&width=800&height=auto&aspect=true", "Rompeviento Everlast con reflex", 44990.00m },
                    { new Guid("0ba6abdd-51eb-4397-9c2a-638fdeedf80d"), 10, "Manguera de plastico", 20, "https://carrefourar.vtexassets.com/arquivos/ids/253554-800-auto?v=637957476573730000&width=800&height=auto&aspect=true", "Manguera premiun 1/2 x 15 mts", 18699.00m },
                    { new Guid("0dc7fcbe-3370-41bd-8b33-02a496c0e8fc"), 4, "El nuevo jabón líquido Skip para Diluir Bio-Enzimas, tiene una nueva tecnología que garantiza la superioridad en la limpieza y cuidado de las fibras, asegurando un impacto positivo en el planeta. El nuevo Skip líquido para diluir ofrece el mismo cuidado de siempre pero de una forma más conveniente para el consumidor: - MÁS ECONÓMICO: 20% de ahorro por lavado. - MÁS PRÁCTICO: No requiere cambios en la dosificación. Es más facil de transportar y almacenar. - MÁS ECOLÓGICO: Fórmula con Activo biodegradable. Menos uso de plástico Botella hecha con plástico reciclado y 100% reciclable. Skip está en constante evolución para ofrecer la mejor tecnología en el lavado y cuidado de la ropa. Además, colores más vivos y duraderos, previene pelotitas, elimina manchas y cuida texturas. Las nuevas tecnologías de los jabones líquidos Skip nos ayudan a consumir inteligentemente, optimizando el uso de energía, disminuyendo residuos e incluso siendo más económico. Y Skip siendo líder en innovación tecnológica tiene credenciales para este lanzamiento. ¿Cómo se utiliza? 1) Coloque agua corriente hasta la marca (2,5 litros) en una botella vacía de Skip 3L 2) Coloque todo el contenido (500 ml) del Jabón líquido Skip para diluir dentro de la botella de 3 L. 3) Cierre la botella y agite hasta lograr una apariencia uniforme (aproximadamente 5 veces). Deje reposar unos minutos hasta que baje la espuma. 4) Dosifique 100?ml para una carga de ropa completa o 150 ml para ropa muy sucia. 500 ml rinde 3L (30 lavados). El lavado perfecto de tu ropa es muy simple.", 20, "https://ardiaprod.vtexassets.com/arquivos/ids/266069-800-auto?v=638322446075400000&width=800&height=auto&aspect=true", "Jabón Líquido para Diluir Skip Bio-Enzimas Tecnologia superior en limpieza y cuidado 500 Ml.", 6525.00m },
                    { new Guid("0e6931d8-23de-47d0-9f90-be31048c5f34"), 4, "Sofá cama Criqueto negro con patas frontales de plásticos. Patas de Metal negro y Espuma cubierto de PU.", 25, "https://carrefourar.vtexassets.com/arquivos/ids/164655-1600-auto?v=637467714147330000&width=1600&height=auto&aspect=true", "Sofá cama Criqueto negro", 199900.00m },
                    { new Guid("1c1db56c-2885-42ef-861e-e4530fdf624b"), 1, "Certificado de Seguridad Eléctrica: DC-E-S23-047.19 IRAM - Instituto Argentino de Normalizacion y Certificación. INFORMACIÓN IMPORTANTE: > Tenemos stock de todos nuestros productos > Envío gratis a todo el país (excepto T. del Fuego) > Plazo de entrega: - CABA y GBA: hasta 8 días hábiles - Interior: hasta 12 días hábiles > Sólo realizamos ventas a consumidor final, motivo por el que únicamente emitimos Factura “B”. -------------------------------------- Campana Extractora 60 Cm Inox Modelo: WAI62AR Campana de pared de 60 cm con diseño moderno y sofisticado. Con salida al exterior, alta capacidad de succión y motor doble turbina que te permite un mayor ahorro de energía. - Cocina libre de olores Cuenta con 3 velocidades de aspiración que te permiten regular el nivel de potencia de acuerdo a los distintos tipos de comida que estés preparando, adaptándose fácilmente a tus necesidades. - Comodidad y visibilidad Posee 2 lámparas halógenas te brindan luminosidad sobre tu cocina y te facilitan la visibilidad para hacerte más cómodo y placentero el momento de cocinar. ESPECIFICACIONES TÉCNICAS - Marca: Whirlpool - Color: Inoxidable - Dimensiones (cm) Alto 62 x Ancho 60 x Profundidad 49 - Peso: 11kg - Capacidad: 450 m3/h - 2 Lámparas halógenas de 28 watts - 3 Velocidades de aspiración - Botonera tipo pulsante - Garantía de Fábrica: 12 meses - Origen: Industria Argentina", 30, "https://ardiaprod.vtexassets.com/arquivos/ids/170362-800-auto?v=637314837200870000&width=800&height=auto&aspect=true", "Campana Extractora Whirlpool 60 CM", 689999.00m },
                    { new Guid("1cd950d8-a5b2-44b8-badb-7bb1b60d86b6"), 5, "Crema corporal", 15, "https://carrefourar.vtexassets.com/arquivos/ids/318455-800-auto?v=638180448741070000&width=800&height=auto&aspect=true", "Crema corporal hidratante Nivea milk nutritiva 5 en 1 para piel extra seca x 250 ml.", 3865.00m },
                    { new Guid("1f7ae2b7-f120-4ba8-bd8f-aee800930154"), 7, "Camioneta de juguete", 15, "https://carrefourar.vtexassets.com/arquivos/ids/363556-800-auto?v=638260818267430000&width=800&height=auto&aspect=true", "Camioneta a fricción spiderman chico", 2990.00m },
                    { new Guid("2ad79d2d-a5f7-415b-bc88-050f7c146def"), 3, "Buzo frisa", 25, "https://carrefourar.vtexassets.com/arquivos/ids/492835-800-auto?v=638464821124700000&width=800&height=auto&aspect=true", "Buzo frisa Tex Basic", 19990.00m },
                    { new Guid("2c6f57e5-cceb-4287-8256-6342da6f336e"), 6, "Colchoneta", 25, "https://carrefourar.vtexassets.com/arquivos/ids/262732-800-auto?v=638040329498330000&width=800&height=auto&aspect=true", "Colchoneta plegable Quuz 1 x 0.50 negra", 13990.00m },
                    { new Guid("2e8f41c9-7d01-4242-bd2e-89941deb4d5c"), 4, "Limpiador CIF EXPERT Antigrasa (450 ml) es un desengrasante líquido que con su innovadora tecnología brinda una limpieza fácil y rápida, ayudando a eliminar hasta la grasa quemada.", 25, "https://ardiaprod.vtexassets.com/arquivos/ids/285928-800-auto?v=638362508968370000&width=800&height=auto&aspect=true", "Limpiador Antigrasa Expert Cif 450 Ml.", 975.00m },
                    { new Guid("2eaf0656-495d-4d49-b5fd-a1b11bf563dd"), 8, "LA SERENÍSIMA CLÁSICO, postre, chocolate. El sabor y la cremosidad con la calidad que ya conoces, un postre ideal para toda la familia. POSTRE 95gr. Postre sabor a chocolate fortificado con vitaminas A, D y ácido fólico libre de gluten. Sin TACC.", 5, "https://ardiaprod.vtexassets.com/arquivos/ids/291428-800-auto?v=638443189318800000&width=800&height=auto&aspect=true", "Postre Chocolate La Serenisima 95 Gr.", 805.00m },
                    { new Guid("3e023694-d9da-4294-a31e-47d2e52e5692"), 3, "Pantalon frisa", 35, "https://carrefourar.vtexassets.com/arquivos/ids/496568-800-auto?v=638476848881900000&width=800&height=auto&aspect=true", "Pantalón frisa Everlast", 44990.00m },
                    { new Guid("3e4a5dcf-1c3f-4d35-b7cf-6992e5b724eb"), 8, "Las Papas fritas Lays Clásicas están hechas con solo 3 ingredientes: papa, aceite y sal. Están sazonadas a la perfección. Tienen una textura crujiente y poseen un sabor único. Lays®, las papas fritas preferidas de los argentinos, estarán presente en cada reunión y en cada encuentro con amigos con exquisitos sabores. Disfruta de este snack salado y acaba con tu antojo.", 20, "https://ardiaprod.vtexassets.com/arquivos/ids/290152-800-auto?v=638428254526570000&width=800&height=auto&aspect=true", "Papas Fritas Lays Clásicas X 134 Gr.", 3465.00m },
                    { new Guid("3e75c184-1e5b-45ee-a52d-a3b78bf5ec80"), 4, "El módulo de un cuerpo LE076 es un producto de la línea industrial, logrado por su acabado rústico y la combinación con lo natural de la materia prima. El diseño está compuesto por un cuerpo principal de melamina y patas de caño redondo curvado. Este mueble posee seis divisiones internas, dos cerradas con puertas y cuatro libres. Para la apertura de estas puertas nos encontramos con unos tiradores de color negro los cuales acompañan con la estetica discreta del diseño. El módulo está pensado en la multifuncionalidad, ya que puede ejercer varios usos dependiendo del ambiente en el que se lo utilice, en la cocina como despensero, en el living como contenedor, en el comedor como vajillero, en el estudio como biblioteca, en la habitación de los niños como porta juguetes, antebaño y baño como toallero y en el lavadero como organizador de elementos de limpieza o simplemente para objetos de decoración en el ambiente que más se desee. Este mueble viene desarmado y preparado para ensamblar de manera rápida y simple, por lo que dentro de la caja tenemos los correspondientes instructivos. Lo podes encontrar en las combinaciones de color atakama en su totalidad y las patas en color negras.", 60, "https://carrefourar.vtexassets.com/arquivos/ids/315618-1600-auto?v=638175094875270000&width=1600&height=auto&aspect=true", "Modulo 2 cuerpos línea industrial", 94399.00m },
                    { new Guid("4a387e2a-6b17-4d38-a8f0-709f1ff37385"), 3, "Remera reciclada", 70, "https://carrefourar.vtexassets.com/arquivos/ids/495623-800-auto?v=638476787294530000&width=800&height=auto&aspect=true", "Remera deporte reciclada Everlast", 18990.00m },
                    { new Guid("4bfc1e85-c406-4b9a-a545-8a90e81665d6"), 1, "INFORMACIÓN IMPORTANTE: > Tenemos stock de todos nuestros productos > Envío gratis a todo el país (excepto T. del Fuego) > Plazo de entrega: - CABA y GBA: hasta 8 días hábiles - Interior: hasta 12 días hábiles > Sólo realizamos ventas a consumidor final, motivo por el que únicamente emitimos Factura “B”. -------------------------------------- Heladera Whirlpool - Retro - 76 Litros Rojo Modelo: WRA09R1 Frigobar Whirlpool Retro roja rescata el diseño elegante de los años 50 con sus patas cromadas y un logo de época. Es compacto pero tiene espacio para todo lo que precisas guardar y posee compartimentos modulares, rack para latas y congelador. - Congelador El congelador del frigobar Retro es perfecto para almacenar pequeños alimentos o botellas. - Todo en su lugar Con los compartimentos modulares es posible organizar y personalizar el interior de tu frigobar Retro, que cuenta con: rack para latas, anaquel fijo en su puerta, compartimento extra frío y un cajón multiuso. - Pies removibles Sus pies de cromo que cuidan el piso al evitar ralladuras, pueden ser removidos para utilizar la heladera en formato built in. ESPECIFICACIONES TÉCNICAS - Marca: Whirlpool - Color: Rojo - Capacidad: 76 lts - Dimensiones (cm) Alto 80,7 x Ancho 48,2 x Profundidad 51,9 - Dimensiones con embalaje (cm) Alto 86 x Ancho 51 x Profundidad 57 - Peso: 29 kg - Peso con embalaje: 30 kg - Eficiencia Energética: A - Garantía de Fábrica: 12 meses - Origen: Brasil", 45, "https://ardiaprod.vtexassets.com/arquivos/ids/170531-800-auto?v=637314847891400000&width=800&height=auto&aspect=true", "Heladera Whirlpool Retro 76 Lts Roja", 781299.00m },
                    { new Guid("4c945121-f6bd-4546-bdca-b36d79c2fcb1"), 4, "ÉCCOLE es un adhesivo para zapatillas de consistencia tipo gel, incoloro y fácil de usar. Adhiere rápidamente sobre los materiales más utilizados en la fabricación de zapatillas tales como cuero, telas y ciertos materiales plásticos.Se utiliza para realizar arreglos en punteras, bordes, talones, suelas y capelladas.", 25, "https://ardiaprod.vtexassets.com/arquivos/ids/277406-800-auto?v=638322603927470000&width=800&height=auto&aspect=true", "Adhesivo para Zapatillas Éccole 9 Gr.", 3860.00m },
                    { new Guid("4edc4eab-a983-47aa-ad81-eaf3fa5a1b60"), 1, "Certificado de Seguridad Eléctrica: DC-E-A39-043.3 - IRAM - Instituto Argentino de Normalizacion y Certificación. Ariston dedica el mayor cuidado al diseño de todos sus anafes con detalles de terminacion ergonomicos y durables. El anafe mixto, cuenta con un diseño unico. Sus cuatro hornallas a gas, una con triple corona, y dos zonas de cocción radiante vitroceramica, lo hacen práctico y elegante.", 15, "https://ardiaprod.vtexassets.com/arquivos/ids/195825-800-auto?v=637541836659970000&width=800&height=auto&aspect=true", "Anafe Mixto Empotrable con diseño italiano", 850000.00m },
                    { new Guid("544f6182-bb5b-4aa1-b215-9e5935b906fc"), 8, "Los Nachos Doritos son productos de copetín a base de maíz y con un intenso sabor a queso. Estos snacks crujientes son perfectos para acompañarlos con cheddar, Gr.uacamole o cualquier otro tipo de salsa que tengas a mano. Ideales para compartir en cualquier ocasión, como snacks o aperitivos en tus fiestas. Probá los Doritos como más te Gr.uste y sorprendete con su delicioso sabor.", 10, "https://ardiaprod.vtexassets.com/arquivos/ids/290257-800-auto?v=638428255891200000&width=800&height=auto&aspect=true", "Nachos Sabor A Queso Doritos X 77 Gr.", 2090.00m },
                    { new Guid("5db4c938-3b40-410f-9197-d62b9a2fcc34"), 2, "Control Inalámbrico DualSense™, Almacenamiento 825GB SSD, Base, Cable HDMI®, Cable AC, Cable USB, Manuales, ASTRO’s PLAYROOM (Juego pre-instalado. La consola puede necesitar actualizarse a la última versión de software disponible. Se requiere una conexión a Internet.)", 45, "https://carrefourar.vtexassets.com/arquivos/ids/411702-1600-auto?v=638348759090700000&width=1600&height=auto&aspect=true", "Consola PS5 HW digital God Of War Bundle", 999999.00m },
                    { new Guid("5fae93cf-95ba-4030-9244-23d8522b422d"), 7, "Pistola de dardos de espuma", 15, "https://carrefourar.vtexassets.com/arquivos/ids/492271-800-auto?v=638461087104970000&width=800&height=auto&aspect=true", "Set pistolas Nerf lanza dardos elite 2.0", 39990.00m },
                    { new Guid("60b094a9-a89b-49a3-9639-4b28052856cf"), 8, "Los Nuevos Vegetales Deshidratados de Knorr son 100% vegetales naturales deshidratados. Son muy fáciles de usar, solamente tenes que hidratarlos por 3 minutos y ya están listos para usar. Agregalos también a tus salsas, arroz y otras preparaciones que contengan agua durante la cocción.", 30, "https://ardiaprod.vtexassets.com/arquivos/ids/279550-800-auto?v=638322629936030000&width=800&height=auto&aspect=true", "Zanahoria Deshidratada Knorr 100 Gr.", 1260.00m },
                    { new Guid("67fa41f9-c34e-4e98-9998-8976de204479"), 7, "Peluche", 30, "https://carrefourar.vtexassets.com/arquivos/ids/494837-800-auto?v=638470795091900000&width=800&height=auto&aspect=true", "Peluche pato de 40 cm", 17990.00m },
                    { new Guid("69cd81db-6a3a-4f5c-85ff-0afa1a43dbe1"), 5, "Los Apósitos adhesivos Curitas Tela elástica son ideales para cubrir todo tipo de heridas pequeñas. Las vendas se estiran y se adaptan a los diferentes movimientos de la piel. Además, poseen una almohadilla antiadherente que protege la herida y la deja respirar. Cabe destacar que las Curitas están disponibles en distintos tamaños para poder cubrir completamente la lesión y/o en paños para poder cortarse a la medida necesaria. Se adecúan a todo tipo de piel.", 30, "https://carrefourar.vtexassets.com/arquivos/ids/240325-800-auto?v=637847884090900000&width=800&height=auto&aspect=true", "Apósitos adhesivos Curitas tela elástica para todo tipo de piel x 20 uni", 1120.00m },
                    { new Guid("813a2b32-d783-4bb3-baba-fdf0d1d448ea"), 2, "Notebook", 55, "https://carrefourar.vtexassets.com/arquivos/ids/395378-1600-auto?v=638327142469700000&width=1600&height=auto&aspect=true", "Notebook Noblex 14 HD CEL N4020C / 4GB/ 128GB SSD", 563019.00m },
                    { new Guid("838b60bd-22d0-4077-a7ab-97b59f1d2784"), 7, "Globos para agua", 10, "https://carrefourar.vtexassets.com/arquivos/ids/161262-800-auto?v=637467656311630000&width=800&height=auto&aspect=true", "Globos para agua Bombuchas x 100 uni", 749.00m },
                    { new Guid("86045dce-5f03-4239-9755-590ba429eab7"), 6, "Pesa", 15, "https://carrefourar.vtexassets.com/arquivos/ids/254508-800-auto?v=637973766158400000&width=800&height=auto&aspect=true", "Pesa grande recargable Quuz", 1990.00m },
                    { new Guid("91c50931-2b2a-486e-a3e4-be0f24b1a0ee"), 2, "Cargador | Data Cable | Guia de Inicio Rapido | Eject Pin", 20, "https://carrefourar.vtexassets.com/arquivos/ids/266137-1600-auto?v=638050042967430000&width=1600&height=auto&aspect=true", "Celular libre Samsung Galaxy A04 128GB negro", 314000.00m },
                    { new Guid("9b35a6cc-d4a2-432a-bda0-c15e9e01c7c6"), 9, "Aventuras para colorear de los personajes de Pokemon.", 15, "https://carrefourar.vtexassets.com/arquivos/ids/379376-800-auto?v=638313642448730000&width=800&height=auto&aspect=true", "Libro Pokemon, aventuras para colorear", 3999.00m },
                    { new Guid("9daeaaec-c765-461e-bd72-1126c0527834"), 4, "Una lunchera", 5, "https://ardiaprod.vtexassets.com/arquivos/ids/271057-800-auto?v=638322514887730000&width=800&height=auto&aspect=true", "Lunchera Escolar Atom 1 Un.", 15000.00m },
                    { new Guid("a1fd9975-eb5f-49a5-8d16-936305d5837d"), 1, "Margarina 100 % vegetal con 30 % menos grasa. Es ideal para untar por su perfil delicado y textura aireada.", 15, "https://ardiaprod.vtexassets.com/arquivos/ids/285404-800-auto?v=638355650539730000&width=800&height=auto&aspect=true", "Margarina Dánica Soft Light en pote 200 Gr.", 1350.00m },
                    { new Guid("a3141a9e-000c-42d0-80d9-8f627e10bc68"), 1, "Dimensiones producto 62 x 110 x 66 cm (ancho x alto x profundidad) Dimensiones con embalaje 67 x 114 x 71 cm (ancho x alto x profundidad) Capacidad bruta 11KG Color Blanco Tipo (abertura de puerta) Superior con sistema soft close en bisgras. Puerta Vidrio templado Niveles de agua 5 (4 + sensado automático) Ciclos Anti-pelusas Antialérgico Remoción de manchas Pro Ropa de mascotas Diario Blanca Color Delicado Jeans Edredón Limpieza de tambor Sanitizar (ropa) Lavado Express Ciclos manuales: Remojar Lavar Enjuagar Desagotar y Centrifugar Filtro Sí Sexto Sentido No. Incluye otras funciones inteligentes. Otras funciones inteligentes Sí: Sensado automático Origen Argentina Sistema de lavado Impeller. Eje vertical, carga superior. Revoluciones por minuto 700 RPM Peso 41.6 KG (neto) Eficiencia Energética / Clase Climática A Pies Niveladores Sí Ruedas No Display Sí Control de temperatura Sí, 3 Alarma de puerta No Traba de seguridad Sí Antiarrugas Sí Tambor Acero inoxidable Garantía 12 meses Consumo de agua 195 Litros Media carga No. Incluye niveles de agua en el panel para seleccionar manualmente. Eficacia de Lavado A Eficacia del Centrifugado C Bomba de desagote Sí Lavado a Mano Incluye ciclo de lavado delicado Entrada Doble de agua Sí Exclusión de Centrifugado Incluye opción antiarrugas Agua fría / caliente Sí Tipo Doble entrada País de destino ARGENTINA Otras Características Incluye: Tecnología de detección automática del tamaño de la carga (sensado automático) Inicio diferido Bloqueo del panel Inluye filtro y lifters 10 años de garantía limitada en el motor* 5 años de garantía limitada en la tarjeta de interfaz", 25, "https://ardiaprod.vtexassets.com/arquivos/ids/230336-800-auto?v=638018938547830000&width=800&height=auto&aspect=true", "Lavarropas carga superior 11 KG blanco", 1199999.00m },
                    { new Guid("a48b1971-710b-4362-a1f0-44edac0f1e6f"), 1, "Certificado de Seguridad Eléctrica: DC-E-A39-063.1 IRAM - Instituto Argentino de Normalizacion y Certificación. Microondas Empotrable con Gill Ariston", 50, "https://ardiaprod.vtexassets.com/arquivos/ids/196158-800-auto?v=637541842581470000&width=800&height=auto&aspect=true", "Microondas Empotrable con Gill Ariston", 1274999.00m },
                    { new Guid("b4e2fa4c-3c3b-4c32-9381-1aa2ba463774"), 9, "Descubre la magia de la Granja de Zenón con esta encantadora colección de libros para pintar en formato de bloc. Con adorables ilustraciones, juegos y actividades, estos libros invitan a los niños a sumergirse en un mundo colorido y estimulante. Ideales para fomentar la creatividad y desarrollar habilidades mientras se divierten con los entrañables personajes.", 5, "https://carrefourar.vtexassets.com/arquivos/ids/473084-800-auto?v=638429152831970000&width=800&height=auto&aspect=true", "Libro reino infantil-maxi color naranja", 4999.00m },
                    { new Guid("b5e7cf90-d59b-4014-8e69-4d92c74280bd"), 10, "- Círculo parcial o completo - Producto: Aspersor - Color: Negro (con boquillas de bronce) - Conexión: 3/4 - Tipo de rosca: Macho - Presión: 2 - 4 bar - Radio: 12 - 14 m. - Caudal: 1500 - 2300 litros/hora - Marca: Maison", 30, "https://carrefourar.vtexassets.com/arquivos/ids/397636-800-auto?v=638333260525730000&width=800&height=auto&aspect=true", "Aspersor de impacto 3/4'' para riego sectorizable 14 m.", 11426.00m },
                    { new Guid("b610f4e8-dc5d-4984-a5ca-6c88bb27ef5b"), 6, "Tubo de pelotas", 10, "https://carrefourar.vtexassets.com/arquivos/ids/254419-800-auto?v=637973765357800000&width=800&height=auto&aspect=true", "Tubo pelotas de tenis Top life x 4", 8990.00m },
                    { new Guid("b946dc56-017a-4909-9e74-3ad0514ae5d7"), 10, "El Controlador Automático de Flujo (CAF) activa la bomba al detectar circulación de agua y la apaga en el instante que deja de fluir. No mantiene las cañerías presurizadas, reduciendo la probabilidad de roturas; y no se activa ante pequeñas pérdidas. El CAF requiere una presión mínima de ingreso de 0,05 Bar o tiene que haber entre la base del tanque y grifo / ducha mas cercana 50 cm (0,50 mca). - Evita que las cañerías permanezcan presurizadas - Impide el funcionamiento en seco - No enciende la bomba con pequeñas pérdidas - Totalmente silenciosa - Requiere presión mínima de ingreso de 0.02 bar o permanecer instalado a 0,2 metros por debajo del tanque elevado - Conexiones roscadas de 1 - Montaje directo sobre bombas de hasta 1,5 Hp - Alimentación: 220 V - Frecuencia: 50 / 60 Hz - Corriente máxima: 16 A - Temperatura máxima del agua: 60º C - Presión máxima de uso: 10 bar - Conexión de entrada: 1” - Conexión de salida: 1” - Grado de protección: IP 65 - Incluye Manual - Marca: Pluvius - Modelo: CAF 396 - Garantía: 6 meses", 30, "https://carrefourar.vtexassets.com/arquivos/ids/390844-800-auto?v=638321327057730000&width=800&height=auto&aspect=true", "Controlador automático de presion presurizador Pluvius CAF", 56350.00m },
                    { new Guid("bd72cf0b-c84a-4844-acc6-fc6a5778257b"), 9, "Actividades para hacer jugando, edición trazos.", 10, "https://carrefourar.vtexassets.com/arquivos/ids/461153-800-auto?v=638415413922430000&width=800&height=auto&aspect=true", "Libro Jugamos Con Numeros", 1800.00m },
                    { new Guid("c38325c7-e90d-482a-b46e-5e210a98f96f"), 3, "Pantalon", 15, "https://carrefourar.vtexassets.com/arquivos/ids/496200-800-auto?v=638476842709700000&width=800&height=auto&aspect=true", "Pantalon trekking Tex", 54990.00m },
                    { new Guid("d2c9f26d-0ef3-484d-8f70-dbd438d84f22"), 1, "Certificado de Seguridad Eléctrica: DC-E-W2-347.1 IRAM - Instituto Argentino de Normalizacion y Certificación. Heladera Whirlpool No Frost Inverter 443 litros", 30, "https://ardiaprod.vtexassets.com/arquivos/ids/196892-800-auto?v=637553146106530000&width=800&height=auto&aspect=true", "Heladera Whirlpool No Frost Inverter 443 litros", 3079999.00m },
                    { new Guid("dd532a3f-1828-48b2-bbf5-b6793595167a"), 5, "Panuelos", 25, "https://carrefourar.vtexassets.com/arquivos/ids/416754-800-auto?v=638355808039530000&width=800&height=auto&aspect=true", "Pañuelos descartables Elite soft touch x6 10 uni", 2000.00m },
                    { new Guid("e611ae9c-fbcf-4b82-8e37-1e725f9eede7"), 4, "Lampara led", 10, "https://ardiaprod.vtexassets.com/arquivos/ids/276111-800-auto?v=638322588095230000&width=800&height=auto&aspect=true", "Lámpara Led Candela Fría 12w 1 Un.", 2245.00m },
                    { new Guid("ff02900c-1dd3-4d7e-9e90-beaa85299342"), 1, "Certificado de Seguridad Eléctrica: Cocina : DC-E-M24-012.1 IRAM - Instituto Argentino de Normalizacion y Certificación / Campana : DC-E-S23-047.19 IRAM - Instituto Argentino de Normalizacion y Certificación. Combo Whirlpool Cocina y Campana", 40, "https://ardiaprod.vtexassets.com/arquivos/ids/205373-800-auto?v=637613438701100000&width=800&height=auto&aspect=true", "Combo Whirlpool Cocina y Campana", 1629998.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Category",
                table: "Product",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_Product",
                table: "SaleProduct",
                column: "Product");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_Sale",
                table: "SaleProduct",
                column: "Sale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleProduct");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
