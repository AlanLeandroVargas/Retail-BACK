using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Persistence;

public class RetailContext : DbContext
{     
    public DbSet<Category> Categories {get;set;}
    public DbSet<Product> Products {get;set;}
    public DbSet<Sale> Sales {get;set;}
    public DbSet<SaleProduct> SaleProducts {get;set;}

    public RetailContext(DbContextOptions<RetailContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var category1 = new Category(1, "Electrodomésticos");
        var category2 = new Category(2, "Tecnología y Electrónica");
        var category3 = new Category(3, "Moda y Accesorios");
        var category4 = new Category(4, "Hogar y Decoración");
        var category5 = new Category(5, "Salud y Belleza");
        var category6 = new Category(6, "Deportes y Ocio");
        var category7 = new Category(7, "Juguetes y Juegos");
        var category8 = new Category(8, "Alimentos y Bebidas");
        var category9 = new Category(9, "Libros y Material Educativo");
        var category10 = new Category(10, "Jardinería y Bricolaje");


        modelBuilder.Entity<Sale>(entity => {
            entity.ToTable("Sale");
            entity.HasKey(s => s.SaleId);
            
            entity.Property(si => si.SaleId).ValueGeneratedOnAdd();
            entity.Property(tp => tp.TotalPay).IsRequired();
            entity.Property(st => st.Subtotal).IsRequired();
            entity.Property(td => td.TotalDiscount).IsRequired();
            entity.Property(t => t.Taxes).IsRequired();
            entity.Property(d => d.Date).IsRequired();
        });

        modelBuilder.Entity<Product>(entity =>{
            entity.ToTable("Product");
            entity.HasKey(e => e.ProductId);
            entity.HasOne<Category>(sc => sc.CategoryInstance)
                    .WithMany(ad => ad.Products)
                    .HasForeignKey(ad => ad.Category)
                    .IsRequired();

            entity.Property(p => p.Name)
                    .HasMaxLength(100)
                    .IsRequired();
            entity.Property(pd => pd.Description).HasMaxLength(int.MaxValue);
            entity.Property(pr => pr.Price).IsRequired();
            entity.Property(pi => pi.ImageUrl)
                    .HasMaxLength(int.MaxValue)
                    .IsRequired(false);
        });

        modelBuilder.Entity<SaleProduct>(entity =>{
            entity.ToTable("SaleProduct");
            entity.HasKey(e => e.ShoppingCartId);
            entity.HasOne(sps => sps.SaleInstance)
                    .WithMany(sp => sp.SaleProducts)
                    .HasForeignKey(sp => sp.Sale)
                    .IsRequired();
            entity.HasOne(spp => spp.ProductInstance)
                    .WithMany(sp => sp.SaleProducts)
                    .HasForeignKey(sp => sp.Product)
                    .IsRequired();
            
            entity.Property(t => t.ShoppingCartId).ValueGeneratedOnAdd();
            entity.Property(q => q.Quantity).IsRequired();
            entity.Property(pr => pr.Price).IsRequired();            
        });

        modelBuilder.Entity<Category>(entity =>{
            entity.ToTable("Category");
            entity.HasKey(e => e.CategoryId);

            entity.Property(ci => ci.CategoryId).ValueGeneratedOnAdd();
            entity.Property(cn => cn.Name)
                    .HasMaxLength(100)
                    .IsRequired(false);
        });
        modelBuilder.Entity<Category>().HasData(
            category1,
            category2,
            category3,
            category4,
            category5,
            category6,
            category7,
            category8,
            category9,
            category10
        );
        var product1 = new Product {ProductId = Guid.NewGuid(), Name = "Nachos Sabor A Queso Doritos X 77 Gr.", Description = "Los Nachos Doritos son productos de copetín a base de maíz y con un intenso sabor a queso. Estos snacks crujientes son perfectos para acompañarlos con cheddar, Gr.uacamole o cualquier otro tipo de salsa que tengas a mano. Ideales para compartir en cualquier ocasión, como snacks o aperitivos en tus fiestas. Probá los Doritos como más te Gr.uste y sorprendete con su delicioso sabor.", Price = 2090.00m, Category = 8, Discount = 10, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/290257-800-auto?v=638428255891200000&width=800&height=auto&aspect=true"};
        var product2 = new Product {ProductId = Guid.NewGuid(), Name = "Papas Fritas Lays Clásicas X 134 Gr.", Description = "Las Papas fritas Lays Clásicas están hechas con solo 3 ingredientes: papa, aceite y sal. Están sazonadas a la perfección. Tienen una textura crujiente y poseen un sabor único. Lays®, las papas fritas preferidas de los argentinos, estarán presente en cada reunión y en cada encuentro con amigos con exquisitos sabores. Disfruta de este snack salado y acaba con tu antojo.", Price = 3465.00m, Category = 8, Discount = 20, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/290152-800-auto?v=638428254526570000&width=800&height=auto&aspect=true"};
        var product3 = new Product {ProductId = Guid.NewGuid(), Name = "Zanahoria Deshidratada Knorr 100 Gr.", Description = "Los Nuevos Vegetales Deshidratados de Knorr son 100% vegetales naturales deshidratados. Son muy fáciles de usar, solamente tenes que hidratarlos por 3 minutos y ya están listos para usar. Agregalos también a tus salsas, arroz y otras preparaciones que contengan agua durante la cocción.", Price = 1260.00m, Category = 8, Discount = 30, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/279550-800-auto?v=638322629936030000&width=800&height=auto&aspect=true"};
        var product4 = new Product {ProductId = Guid.NewGuid(), Name = "Margarina Dánica Soft Light en pote 200 Gr.", Description = "Margarina 100 % vegetal con 30 % menos grasa. Es ideal para untar por su perfil delicado y textura aireada.", Price = 1350.00m, Category = 1, Discount = 15, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/285404-800-auto?v=638355650539730000&width=800&height=auto&aspect=true"};
        var product5 = new Product {ProductId = Guid.NewGuid(), Name = "Yogur Bebible Frutilla La Serenísima Clásico 900 Gr.", Description = "LA SERENÍSIMA CLÁSICO, yogur bebible, parcialmente descremado, sabor frutilla.?De la mejor leche salen los mejores yogures, disfrutá un vaso de yogur todos los días. SACHET 900g.?Yogur con probióticos endulzado parcialmente descremado fortificado con zinc y vitaminas A y D sabor frutilla libre de gluten – licuado. Sin TACC.", Price = 2095.00m, Category = 8, Discount = 25, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/287581-800-auto?v=638383484383700000&width=800&height=auto&aspect=true"};
        var product6 = new Product {ProductId = Guid.NewGuid(), Name = "Postre Chocolate La Serenisima 95 Gr.", Description = "LA SERENÍSIMA CLÁSICO, postre, chocolate. El sabor y la cremosidad con la calidad que ya conoces, un postre ideal para toda la familia. POSTRE 95gr. Postre sabor a chocolate fortificado con vitaminas A, D y ácido fólico libre de gluten. Sin TACC.", Price = 805.00m, Category = 8, Discount = 5, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/291428-800-auto?v=638443189318800000&width=800&height=auto&aspect=true"};
        var product7 = new Product {ProductId = Guid.NewGuid(), Name = "Jabón Líquido para Diluir Skip Bio-Enzimas Tecnologia superior en limpieza y cuidado 500 Ml.", Description = "El nuevo jabón líquido Skip para Diluir Bio-Enzimas, tiene una nueva tecnología que garantiza la superioridad en la limpieza y cuidado de las fibras, asegurando un impacto positivo en el planeta. El nuevo Skip líquido para diluir ofrece el mismo cuidado de siempre pero de una forma más conveniente para el consumidor: - MÁS ECONÓMICO: 20% de ahorro por lavado. - MÁS PRÁCTICO: No requiere cambios en la dosificación. Es más facil de transportar y almacenar. - MÁS ECOLÓGICO: Fórmula con Activo biodegradable. Menos uso de plástico Botella hecha con plástico reciclado y 100% reciclable. Skip está en constante evolución para ofrecer la mejor tecnología en el lavado y cuidado de la ropa. Además, colores más vivos y duraderos, previene pelotitas, elimina manchas y cuida texturas. Las nuevas tecnologías de los jabones líquidos Skip nos ayudan a consumir inteligentemente, optimizando el uso de energía, disminuyendo residuos e incluso siendo más económico. Y Skip siendo líder en innovación tecnológica tiene credenciales para este lanzamiento. ¿Cómo se utiliza? 1) Coloque agua corriente hasta la marca (2,5 litros) en una botella vacía de Skip 3L 2) Coloque todo el contenido (500 ml) del Jabón líquido Skip para diluir dentro de la botella de 3 L. 3) Cierre la botella y agite hasta lograr una apariencia uniforme (aproximadamente 5 veces). Deje reposar unos minutos hasta que baje la espuma. 4) Dosifique 100?ml para una carga de ropa completa o 150 ml para ropa muy sucia. 500 ml rinde 3L (30 lavados). El lavado perfecto de tu ropa es muy simple.", Price = 6525.00m, Category = 4, Discount = 20, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/266069-800-auto?v=638322446075400000&width=800&height=auto&aspect=true"};
        var product8 = new Product {ProductId = Guid.NewGuid(), Name = "Limpiador Antigrasa Expert Cif 450 Ml.", Description = "Limpiador CIF EXPERT Antigrasa (450 ml) es un desengrasante líquido que con su innovadora tecnología brinda una limpieza fácil y rápida, ayudando a eliminar hasta la grasa quemada.", Price = 975.00m, Category = 4, Discount = 25, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/285928-800-auto?v=638362508968370000&width=800&height=auto&aspect=true"};
        var product9 = new Product {ProductId = Guid.NewGuid(), Name = "Jabón Líquido Concentrado para Diluir Ala con Bicarbonato 500 Ml.", Description = "En ALA quisimos dar el próximo paso con el nuevo ALA líquido concentrado para diluir con Bicarbonato. La nueva fórmula del jabón líquido concentrado para diluir ALA Ecolavado con Bicarbonato tiene un poder de limpieza superior y agentes de limpieza naturales que remueven las manchas más difíciles, en el primer lavado. Este jabón para diluir combate el mal olor y es ideal para ropa blanca y de color. Además de tener una fragancia duradera. Por otro lado, su fórmula, con una tecnología patentada que se potencia en contacto con el agua y el agregado del bicarbonato que contribuye a mantener la blancura de las prendas, hace de este jabón para ropa un producto de limpieza profunda y superior. Este jabón concentrado rinde 3 L dado que su formato es para diluir. Para prepararlo, solo tenés que llenar una botella vacía de ALA 3 L con 2,5?L de agua potable, agregar el ALA líquido para diluir, mezclar 5 veces de arriba hacia abajo - ¡Y listo, ya tenés tu jabón líquido preparado con la misma consistencia y fragancia riquísima de siempre! Se dosifican 100?ml para una carga de ropa completa o 150?ml para ropa muy sucia. ALA, porque ensuciarse hace bien. Ingresá a www.ala.com.ar y conocé más de nuestros productos.", Price = 6340.00m, Category = 4, Discount = 30, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/286084-800-auto?v=638362510764470000&width=800&height=auto&aspect=true"};
        var product10 = new Product {ProductId = Guid.NewGuid(), Name = "Lunchera Escolar Atom 1 Un.", Description = "Una lunchera", Price = 15000.00m, Category = 4, Discount = 5, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/271057-800-auto?v=638322514887730000&width=800&height=auto&aspect=true"};
        var product11 = new Product {ProductId = Guid.NewGuid(), Name = "Adhesivo para Zapatillas Éccole 9 Gr.", Description = "ÉCCOLE es un adhesivo para zapatillas de consistencia tipo gel, incoloro y fácil de usar. Adhiere rápidamente sobre los materiales más utilizados en la fabricación de zapatillas tales como cuero, telas y ciertos materiales plásticos.Se utiliza para realizar arreglos en punteras, bordes, talones, suelas y capelladas.", Price = 3860.00m, Category = 4, Discount = 25, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/277406-800-auto?v=638322603927470000&width=800&height=auto&aspect=true"};
        var product12 = new Product {ProductId = Guid.NewGuid(), Name = "Lámpara Led Candela Fría 12w 1 Un.", Description = "Lampara led", Price = 2245.00m, Category = 4, Discount = 10, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/276111-800-auto?v=638322588095230000&width=800&height=auto&aspect=true"};
        var product13 = new Product {ProductId = Guid.NewGuid(), Name = "Lavarropas Whirlpool 9,5kg - 1400rpm Blanco Inverter", Description = "Tecnología Principal: Inverter Dimensiones del producto 84,6 x 59,5 x 64,4 cm Dimensiones con embalaje 87,7 x 67,6 x 69,8 cm Capacidad bruta 64 L / 9,5 KG Color Blanco Tipo (abertura de puerta) Frontal Puerta Vidrio/Plástico Ciclos (listar) 24 Ciclos: Rápido 45 Quita Manchas PRO Refrescar con Vapor Lana Edredón Eco EEE Enjuagar y Centrifugar Centrifugar Diario Color Blanca Rápido 15 Delicada Sanitizar Sábanas y toallas + Ciclos adicionales Filtro (sí/no) Si Sexto Sentido Si Origen Argentina Sistema de lavado Frontal Revoluciones por minuto 1400 RPM Peso 79,7 KG Pies Niveladores Si Ruedas No Display Si Control de temperatura Si Alarma de puerta Si Traba de seguridad Si Función Antiarrugas Si Tambor Acero inoxidable Garantía 12 meses + 10 años garantía especial limitada en el motor Tipo de adaptador Argentino Tipo de cable Estándard con enchufe Nivel de ruido N/A Consumo de agua 64 L Media carga Incluye sensado de carga Eficacia de Lavado A Eficacia del Centrifugado B Eficiencia Energética / Clase Climática A++ Bomba de desagote Sí Lavado a Mano Incluye ciclo de lavado delicado Exclusión de Centrifugado Si Agua fría / caliente Incluye calentador interno", Price = 1549999.00m, Category = 1, Discount = 20, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/261242-800-auto?v=638313511734830000&width=800&height=auto&aspect=true"};
        var product14 = new Product {ProductId = Guid.NewGuid(), Name = "Heladera Whirlpool No Frost Inverter 443 litros", Description = "Certificado de Seguridad Eléctrica: DC-E-W2-347.1 IRAM - Instituto Argentino de Normalizacion y Certificación. Heladera Whirlpool No Frost Inverter 443 litros", Price = 3079999.00m, Category = 1, Discount = 30, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/196892-800-auto?v=637553146106530000&width=800&height=auto&aspect=true"};
        var product15 = new Product {ProductId = Guid.NewGuid(), Name = "Microondas Empotrable con Gill Ariston", Description = "Certificado de Seguridad Eléctrica: DC-E-A39-063.1 IRAM - Instituto Argentino de Normalizacion y Certificación. Microondas Empotrable con Gill Ariston", Price = 1274999.00m, Category = 1, Discount = 50, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/196158-800-auto?v=637541842581470000&width=800&height=auto&aspect=true"};
        var product16 = new Product {ProductId = Guid.NewGuid(), Name = "Lavarropas carga superior 11 KG blanco", Description = "Dimensiones producto 62 x 110 x 66 cm (ancho x alto x profundidad) Dimensiones con embalaje 67 x 114 x 71 cm (ancho x alto x profundidad) Capacidad bruta 11KG Color Blanco Tipo (abertura de puerta) Superior con sistema soft close en bisgras. Puerta Vidrio templado Niveles de agua 5 (4 + sensado automático) Ciclos Anti-pelusas Antialérgico Remoción de manchas Pro Ropa de mascotas Diario Blanca Color Delicado Jeans Edredón Limpieza de tambor Sanitizar (ropa) Lavado Express Ciclos manuales: Remojar Lavar Enjuagar Desagotar y Centrifugar Filtro Sí Sexto Sentido No. Incluye otras funciones inteligentes. Otras funciones inteligentes Sí: Sensado automático Origen Argentina Sistema de lavado Impeller. Eje vertical, carga superior. Revoluciones por minuto 700 RPM Peso 41.6 KG (neto) Eficiencia Energética / Clase Climática A Pies Niveladores Sí Ruedas No Display Sí Control de temperatura Sí, 3 Alarma de puerta No Traba de seguridad Sí Antiarrugas Sí Tambor Acero inoxidable Garantía 12 meses Consumo de agua 195 Litros Media carga No. Incluye niveles de agua en el panel para seleccionar manualmente. Eficacia de Lavado A Eficacia del Centrifugado C Bomba de desagote Sí Lavado a Mano Incluye ciclo de lavado delicado Entrada Doble de agua Sí Exclusión de Centrifugado Incluye opción antiarrugas Agua fría / caliente Sí Tipo Doble entrada País de destino ARGENTINA Otras Características Incluye: Tecnología de detección automática del tamaño de la carga (sensado automático) Inicio diferido Bloqueo del panel Inluye filtro y lifters 10 años de garantía limitada en el motor* 5 años de garantía limitada en la tarjeta de interfaz", Price = 1199999.00m, Category = 1, Discount = 25, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/230336-800-auto?v=638018938547830000&width=800&height=auto&aspect=true"};
        var product17 = new Product {ProductId = Guid.NewGuid(), Name = "Anafe Mixto Empotrable con diseño italiano", Description = "Certificado de Seguridad Eléctrica: DC-E-A39-043.3 - IRAM - Instituto Argentino de Normalizacion y Certificación. Ariston dedica el mayor cuidado al diseño de todos sus anafes con detalles de terminacion ergonomicos y durables. El anafe mixto, cuenta con un diseño unico. Sus cuatro hornallas a gas, una con triple corona, y dos zonas de cocción radiante vitroceramica, lo hacen práctico y elegante.", Price = 850000.00m, Category = 1, Discount = 15, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/195825-800-auto?v=637541836659970000&width=800&height=auto&aspect=true"};
        var product18 = new Product {ProductId = Guid.NewGuid(), Name = "Combo Whirlpool Cocina y Campana", Description = "Certificado de Seguridad Eléctrica: Cocina : DC-E-M24-012.1 IRAM - Instituto Argentino de Normalizacion y Certificación / Campana : DC-E-S23-047.19 IRAM - Instituto Argentino de Normalizacion y Certificación. Combo Whirlpool Cocina y Campana", Price = 1629998.00m, Category = 1, Discount = 40, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/205373-800-auto?v=637613438701100000&width=800&height=auto&aspect=true"};
        var product19 = new Product {ProductId = Guid.NewGuid(), Name = "Heladera Whirlpool Retro 76 Lts Roja", Description = "INFORMACIÓN IMPORTANTE: > Tenemos stock de todos nuestros productos > Envío gratis a todo el país (excepto T. del Fuego) > Plazo de entrega: - CABA y GBA: hasta 8 días hábiles - Interior: hasta 12 días hábiles > Sólo realizamos ventas a consumidor final, motivo por el que únicamente emitimos Factura “B”. -------------------------------------- Heladera Whirlpool - Retro - 76 Litros Rojo Modelo: WRA09R1 Frigobar Whirlpool Retro roja rescata el diseño elegante de los años 50 con sus patas cromadas y un logo de época. Es compacto pero tiene espacio para todo lo que precisas guardar y posee compartimentos modulares, rack para latas y congelador. - Congelador El congelador del frigobar Retro es perfecto para almacenar pequeños alimentos o botellas. - Todo en su lugar Con los compartimentos modulares es posible organizar y personalizar el interior de tu frigobar Retro, que cuenta con: rack para latas, anaquel fijo en su puerta, compartimento extra frío y un cajón multiuso. - Pies removibles Sus pies de cromo que cuidan el piso al evitar ralladuras, pueden ser removidos para utilizar la heladera en formato built in. ESPECIFICACIONES TÉCNICAS - Marca: Whirlpool - Color: Rojo - Capacidad: 76 lts - Dimensiones (cm) Alto 80,7 x Ancho 48,2 x Profundidad 51,9 - Dimensiones con embalaje (cm) Alto 86 x Ancho 51 x Profundidad 57 - Peso: 29 kg - Peso con embalaje: 30 kg - Eficiencia Energética: A - Garantía de Fábrica: 12 meses - Origen: Brasil", Price = 781299.00m, Category = 1, Discount = 45, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/170531-800-auto?v=637314847891400000&width=800&height=auto&aspect=true"};
        var product20 = new Product {ProductId = Guid.NewGuid(), Name = "Campana Extractora Whirlpool 60 CM", Description = "Certificado de Seguridad Eléctrica: DC-E-S23-047.19 IRAM - Instituto Argentino de Normalizacion y Certificación. INFORMACIÓN IMPORTANTE: > Tenemos stock de todos nuestros productos > Envío gratis a todo el país (excepto T. del Fuego) > Plazo de entrega: - CABA y GBA: hasta 8 días hábiles - Interior: hasta 12 días hábiles > Sólo realizamos ventas a consumidor final, motivo por el que únicamente emitimos Factura “B”. -------------------------------------- Campana Extractora 60 Cm Inox Modelo: WAI62AR Campana de pared de 60 cm con diseño moderno y sofisticado. Con salida al exterior, alta capacidad de succión y motor doble turbina que te permite un mayor ahorro de energía. - Cocina libre de olores Cuenta con 3 velocidades de aspiración que te permiten regular el nivel de potencia de acuerdo a los distintos tipos de comida que estés preparando, adaptándose fácilmente a tus necesidades. - Comodidad y visibilidad Posee 2 lámparas halógenas te brindan luminosidad sobre tu cocina y te facilitan la visibilidad para hacerte más cómodo y placentero el momento de cocinar. ESPECIFICACIONES TÉCNICAS - Marca: Whirlpool - Color: Inoxidable - Dimensiones (cm) Alto 62 x Ancho 60 x Profundidad 49 - Peso: 11kg - Capacidad: 450 m3/h - 2 Lámparas halógenas de 28 watts - 3 Velocidades de aspiración - Botonera tipo pulsante - Garantía de Fábrica: 12 meses - Origen: Industria Argentina", Price = 689999.00m, Category = 1, Discount = 30, ImageUrl = "https://ardiaprod.vtexassets.com/arquivos/ids/170362-800-auto?v=637314837200870000&width=800&height=auto&aspect=true"};
        var product21 = new Product {ProductId = Guid.NewGuid(), Name = "Remera deporte reciclada Everlast", Description = "Remera reciclada", Price = 18990.00m, Category = 3, Discount = 70, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/495623-800-auto?v=638476787294530000&width=800&height=auto&aspect=true"};
        var product22 = new Product {ProductId = Guid.NewGuid(), Name = "Pantalon trekking Tex", Description = "Pantalon", Price = 54990.00m, Category = 3, Discount = 15, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/496200-800-auto?v=638476842709700000&width=800&height=auto&aspect=true"};
        var product23 = new Product {ProductId = Guid.NewGuid(), Name = "Rompeviento Everlast con reflex", Description = "Rompeviento", Price = 44990.00m, Category = 3, Discount = 30, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/496975-800-auto?v=638476855667000000&width=800&height=auto&aspect=true"};
        var product24 = new Product {ProductId = Guid.NewGuid(), Name = "Pantalón frisa Everlast", Description = "Pantalon frisa", Price = 44990.00m, Category = 3, Discount = 35, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/496568-800-auto?v=638476848881900000&width=800&height=auto&aspect=true"};
        var product25 = new Product {ProductId = Guid.NewGuid(), Name = "Buzo frisa Tex Basic", Description = "Buzo frisa", Price = 19990.00m, Category = 3, Discount = 25, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/492835-800-auto?v=638464821124700000&width=800&height=auto&aspect=true"};
        var product26 = new Product {ProductId = Guid.NewGuid(), Name = "Globos para agua Bombuchas x 100 uni", Description = "Globos para agua", Price = 749.00m, Category = 7, Discount = 10, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/161262-800-auto?v=637467656311630000&width=800&height=auto&aspect=true"};
        var product27 = new Product {ProductId = Guid.NewGuid(), Name = "Set pistolas Nerf lanza dardos elite 2.0", Description = "Pistola de dardos de espuma", Price = 39990.00m, Category = 7, Discount = 15, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/492271-800-auto?v=638461087104970000&width=800&height=auto&aspect=true"};
        var product28 = new Product {ProductId = Guid.NewGuid(), Name = "Peluche pato de 40 cm", Description = "Peluche", Price = 17990.00m, Category = 7, Discount = 30, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/494837-800-auto?v=638470795091900000&width=800&height=auto&aspect=true"};
        var product29 = new Product {ProductId = Guid.NewGuid(), Name = "Camioneta a fricción spiderman chico", Description = "Camioneta de juguete", Price = 2990.00m, Category = 7, Discount = 15, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/363556-800-auto?v=638260818267430000&width=800&height=auto&aspect=true"};
        var product30 = new Product {ProductId = Guid.NewGuid(), Name = "Libro Jugamos Con Numeros", Description = "Actividades para hacer jugando, edición trazos.", Price = 1800.00m, Category = 9, Discount = 10, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/461153-800-auto?v=638415413922430000&width=800&height=auto&aspect=true"};
        var product31 = new Product {ProductId = Guid.NewGuid(), Name = "Libro reino infantil-maxi color naranja", Description = "Descubre la magia de la Granja de Zenón con esta encantadora colección de libros para pintar en formato de bloc. Con adorables ilustraciones, juegos y actividades, estos libros invitan a los niños a sumergirse en un mundo colorido y estimulante. Ideales para fomentar la creatividad y desarrollar habilidades mientras se divierten con los entrañables personajes.", Price = 4999.00m, Category = 9, Discount = 5, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/473084-800-auto?v=638429152831970000&width=800&height=auto&aspect=true"};
        var product32 = new Product {ProductId = Guid.NewGuid(), Name = "Libro Pokemon, aventuras para colorear", Description = "Aventuras para colorear de los personajes de Pokemon.", Price = 3999.00m, Category = 9, Discount = 15, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/379376-800-auto?v=638313642448730000&width=800&height=auto&aspect=true"};
        var product33 = new Product {ProductId = Guid.NewGuid(), Name = "Controlador automático de presion presurizador Pluvius CAF", Description = "El Controlador Automático de Flujo (CAF) activa la bomba al detectar circulación de agua y la apaga en el instante que deja de fluir. No mantiene las cañerías presurizadas, reduciendo la probabilidad de roturas; y no se activa ante pequeñas pérdidas. El CAF requiere una presión mínima de ingreso de 0,05 Bar o tiene que haber entre la base del tanque y grifo / ducha mas cercana 50 cm (0,50 mca). - Evita que las cañerías permanezcan presurizadas - Impide el funcionamiento en seco - No enciende la bomba con pequeñas pérdidas - Totalmente silenciosa - Requiere presión mínima de ingreso de 0.02 bar o permanecer instalado a 0,2 metros por debajo del tanque elevado - Conexiones roscadas de 1 - Montaje directo sobre bombas de hasta 1,5 Hp - Alimentación: 220 V - Frecuencia: 50 / 60 Hz - Corriente máxima: 16 A - Temperatura máxima del agua: 60º C - Presión máxima de uso: 10 bar - Conexión de entrada: 1” - Conexión de salida: 1” - Grado de protección: IP 65 - Incluye Manual - Marca: Pluvius - Modelo: CAF 396 - Garantía: 6 meses", Price = 56350.00m, Category = 10, Discount = 30, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/390844-800-auto?v=638321327057730000&width=800&height=auto&aspect=true"};
        var product34 = new Product {ProductId = Guid.NewGuid(), Name = "Manguera premiun 1/2 x 15 mts", Description = "Manguera de plastico", Price = 18699.00m, Category = 10, Discount = 20, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/253554-800-auto?v=637957476573730000&width=800&height=auto&aspect=true"};
        var product35 = new Product {ProductId = Guid.NewGuid(), Name = "Aspersor de impacto 3/4'' para riego sectorizable 14 m.", Description = "- Círculo parcial o completo - Producto: Aspersor - Color: Negro (con boquillas de bronce) - Conexión: 3/4 - Tipo de rosca: Macho - Presión: 2 - 4 bar - Radio: 12 - 14 m. - Caudal: 1500 - 2300 litros/hora - Marca: Maison", Price = 11426.00m, Category = 10, Discount = 30, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/397636-800-auto?v=638333260525730000&width=800&height=auto&aspect=true"};
        var product36 = new Product {ProductId = Guid.NewGuid(), Name = "Colchoneta plegable Quuz 1 x 0.50 negra", Description = "Colchoneta", Price = 13990.00m, Category = 6, Discount = 25, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/262732-800-auto?v=638040329498330000&width=800&height=auto&aspect=true"};
        var product37 = new Product {ProductId = Guid.NewGuid(), Name = "Pesa grande recargable Quuz", Description = "Pesa", Price = 1990.00m, Category = 6, Discount = 15, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/254508-800-auto?v=637973766158400000&width=800&height=auto&aspect=true"};
        var product38 = new Product {ProductId = Guid.NewGuid(), Name = "Tubo pelotas de tenis Top life x 4", Description = "Tubo de pelotas", Price = 8990.00m, Category = 6, Discount = 10, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/254419-800-auto?v=637973765357800000&width=800&height=auto&aspect=true"};
        var product39 = new Product {ProductId = Guid.NewGuid(), Name = "Pañuelos descartables Elite soft touch x6 10 uni", Description = "Panuelos", Price = 2000.00m, Category = 5, Discount = 25, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/416754-800-auto?v=638355808039530000&width=800&height=auto&aspect=true"};
        var product40 = new Product {ProductId = Guid.NewGuid(), Name = "Apósitos adhesivos Curitas tela elástica para todo tipo de piel x 20 uni", Description = "Los Apósitos adhesivos Curitas Tela elástica son ideales para cubrir todo tipo de heridas pequeñas. Las vendas se estiran y se adaptan a los diferentes movimientos de la piel. Además, poseen una almohadilla antiadherente que protege la herida y la deja respirar. Cabe destacar que las Curitas están disponibles en distintos tamaños para poder cubrir completamente la lesión y/o en paños para poder cortarse a la medida necesaria. Se adecúan a todo tipo de piel.", Price = 1120.00m, Category = 5, Discount = 30, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/240325-800-auto?v=637847884090900000&width=800&height=auto&aspect=true"};
        var product41 = new Product {ProductId = Guid.NewGuid(), Name = "Crema corporal hidratante Nivea milk nutritiva 5 en 1 para piel extra seca x 250 ml.", Description = "Crema corporal", Price = 3865.00m, Category = 5, Discount = 15, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/318455-800-auto?v=638180448741070000&width=800&height=auto&aspect=true"};
        var product42 = new Product {ProductId = Guid.NewGuid(), Name = "Modulo 2 cuerpos línea industrial", Description = "El módulo de un cuerpo LE076 es un producto de la línea industrial, logrado por su acabado rústico y la combinación con lo natural de la materia prima. El diseño está compuesto por un cuerpo principal de melamina y patas de caño redondo curvado. Este mueble posee seis divisiones internas, dos cerradas con puertas y cuatro libres. Para la apertura de estas puertas nos encontramos con unos tiradores de color negro los cuales acompañan con la estetica discreta del diseño. El módulo está pensado en la multifuncionalidad, ya que puede ejercer varios usos dependiendo del ambiente en el que se lo utilice, en la cocina como despensero, en el living como contenedor, en el comedor como vajillero, en el estudio como biblioteca, en la habitación de los niños como porta juguetes, antebaño y baño como toallero y en el lavadero como organizador de elementos de limpieza o simplemente para objetos de decoración en el ambiente que más se desee. Este mueble viene desarmado y preparado para ensamblar de manera rápida y simple, por lo que dentro de la caja tenemos los correspondientes instructivos. Lo podes encontrar en las combinaciones de color atakama en su totalidad y las patas en color negras.", Price = 94399.00m, Category = 4, Discount = 60, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/315618-1600-auto?v=638175094875270000&width=1600&height=auto&aspect=true"};
        var product43 = new Product {ProductId = Guid.NewGuid(), Name = "Sofá cama Criqueto negro", Description = "Sofá cama Criqueto negro con patas frontales de plásticos. Patas de Metal negro y Espuma cubierto de PU.", Price = 199900.00m, Category = 4, Discount = 25, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/164655-1600-auto?v=637467714147330000&width=1600&height=auto&aspect=true"};
        var product44 = new Product {ProductId = Guid.NewGuid(), Name = "Notebook Noblex 14 HD CEL N4020C / 4GB/ 128GB SSD", Description = "Notebook", Price = 563019.00m, Category = 2, Discount = 55, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/395378-1600-auto?v=638327142469700000&width=1600&height=auto&aspect=true"};
        var product45 = new Product {ProductId = Guid.NewGuid(), Name = "Consola PS5 HW digital God Of War Bundle", Description = "Control Inalámbrico DualSense™, Almacenamiento 825GB SSD, Base, Cable HDMI®, Cable AC, Cable USB, Manuales, ASTRO’s PLAYROOM (Juego pre-instalado. La consola puede necesitar actualizarse a la última versión de software disponible. Se requiere una conexión a Internet.)", Price = 999999.00m, Category = 2, Discount = 45, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/411702-1600-auto?v=638348759090700000&width=1600&height=auto&aspect=true"};
        var product46 = new Product {ProductId = Guid.NewGuid(), Name = "Celular libre Samsung Galaxy A04 128GB negro", Description = "Cargador | Data Cable | Guia de Inicio Rapido | Eject Pin", Price = 314000.00m, Category = 2, Discount = 20, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/266137-1600-auto?v=638050042967430000&width=1600&height=auto&aspect=true"};
        var product47 = new Product {ProductId = Guid.NewGuid(), Name = "Celular libre Motorola g23 4gb 128gb blanco", Description = "Celular libre", Price = 399999.00m, Category = 2, Discount = 30, ImageUrl = "https://carrefourar.vtexassets.com/arquivos/ids/367471-1600-auto?v=638283212666130000&width=1600&height=auto&aspect=true"};        

        modelBuilder.Entity<Product>().HasData(
            product1,
            product2,
            product3,
            product4,
            product5,
            product6,
            product7,
            product8,
            product9,
            product10,
            product11,
            product12,
            product13,
            product14,
            product15,
            product16,
            product17,
            product18,
            product19,
            product20,
            product21,
            product22,
            product23,
            product24,
            product25,
            product26,
            product27,
            product28,
            product29,
            product30,
            product31,
            product32,
            product33,
            product34,
            product35,
            product36,
            product37,
            product38,
            product39,
            product40,
            product41,
            product42,
            product43,
            product44,
            product45,
            product46,
            product47                                                                                                                                                                                                                                                                                                                   
        );
    }
} 