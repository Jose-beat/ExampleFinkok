openssl x509 -inform DER -outform PEM -in "C:\Users\WAR-PLANE\Desktop\Practicas Profesionales\Proyectos\ExampleFinkok\ExampleFinkok\ExampleFinkok\wwwroot\certifiedDocs\CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753s.cer" -pubkey -out "C:\Users\WAR-PLANE\Desktop\Practicas Profesionales\Proyectos\ExampleFinkok\ExampleFinkok\ExampleFinkok\wwwroot\compiledFiles\CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753s.cer.pem"
openssl pkcs8 -inform DER -in "C:\Users\WAR-PLANE\Desktop\Practicas Profesionales\Proyectos\ExampleFinkok\ExampleFinkok\ExampleFinkok\wwwroot\certifiedDocs\CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753.key" -passin pass:12345678a -out "C:\Users\WAR-PLANE\Desktop\Practicas Profesionales\Proyectos\ExampleFinkok\ExampleFinkok\ExampleFinkok\wwwroot\compiledFiles\CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753.key.pem"
openssl rsa -in "C:\Users\WAR-PLANE\Desktop\Practicas Profesionales\Proyectos\ExampleFinkok\ExampleFinkok\ExampleFinkok\wwwroot\compiledFiles\CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753.key.pem" -des3 -out "C:\Users\WAR-PLANE\Desktop\Practicas Profesionales\Proyectos\ExampleFinkok\ExampleFinkok\ExampleFinkok\wwwroot\compiledFiles\CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753.key.enc" -passout pass:The_Beatles1960