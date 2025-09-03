# Fruzzy
Tecnologia embarcada:

- C# .Net Core 8/5.0
- SQL Express
- ORM: Entity Framework
- Front em Razor Pages
- IISExpress (Necessário para o funcionamento do IronOCR) 

Principais Nuggets Instalados:
- FuzzySharp
- IronOCR
- ASPOSE OCR
- PDF Reader
- XMLWorkbook
- Microsoft.RecognizerText.DateTime 

Extensões para o visual Studio 
- Conveyor 

## Endereço para criação de novos usuários 
- Para aplicações em desenvolvimento a criação de novos usuários deve acontecer neste endereço -> https://localhost:44312/Identity/Account/Register  
- Para aplicações em produção, a criação de novos usuários deve acontecer neste endereço -> https://krjdoc:45455/Identity/Account/Register 

## Base de dados e contexto 
O sistema está implementado no formato Database-First, não sendo necessário script para a criação do banco de dados. Apenas a inicialização do sistema com a variável de ambiente no modo de desenvolvimento. Ele irá apresentar a tela de login e escrever um email e senha 
aleatório. Ele apresentará a mensagem de ausência do banco de dados e apresentará uma mensagem de aplicar migration. Escolha a opção com a lista de maior opção. 

Em produção o sistema utiliza um banco de dados em sqlexpress com o banco, já configurado com a autenticação do Windows do sistema AD Fruzzy.  

## Bases em JSONs 
Existe uma pasta com um conjunto de arquivos nos formatos json. Eles tem as configurações de vários parâmetros para a leitura e identificação dos documentos que serão lidos. O apontamento do caminho da pasta leitura desses arquivos no formato json, deve ser feito no arquivo appsettings.json no parâmetro expressionjsons. 

## Formatação de datas 
Por conta da inteligência de leitura de datas da Microsoft, foi necessário “desserializar” as datas em arquivos separados pelo nome do arquivo no formato JSON.. O apontamento do caminho da pasta de escrita desses arquivos no formato json, deve ser feito no arquivo `appsettings.json`. 

## Operations 
É permitido apenas uma operação em andamento por vez no sistema para garantirmos que não teremos “estouros” de memória por conta de um problema de alto processamento. Caso a operação esteja presa e o sistema com baixo processamento (abaixo de 6%), pode ser executado este comando para limpar alguma operação que tenha ficado presa. 

```
Update dbo.Operation set ready = ‘true’ Where ready = ‘false’. 
```

## Fuzzyficação
