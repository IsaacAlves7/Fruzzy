> Versículo chave: "Consagre ao Senhor tudo o que você faz, e os seus planos serão bem-sucedidos." - Provérbios 16:3

# 🧐 Fruzzy
A ideia por trás do **Fruzzy** é inovadora para Engenharia de Dados e ETL/ELT, pois atua como um sistema inteligente capaz de lidar com múltiplos arquivos e executar todo o fluxo de trabalho ETL/ELT de ponta a ponta. Em vez de focar em etapas isoladas como extração, transformação ou carregamento separado, ele reúne tudo em um pipeline unificado e automatizado, onde documentos podem ser ingeridos em escala e processados em saídas estruturadas em formatos como JSON ou Markdown. Isso o torna especialmente útil para fluxos de trabalho modernos de dados e sistemas de IA, onde dados não estruturados precisam ser continuamente transformados em informações utilizáveis e legíveis por máquinas.

Antes, o repositório era de um sistema que realizava OCR em documentos PDF, extraia dados estruturados e os exportava automaticamente para planilhas do Excel para automação e análise. Com ele era possível ler mais de 100 PDFs e escrever todos os dados e relatórios em uma única planilha do Excel.

A imagem abaixo ilustra de forma simples como é feito o processamento do OCR, e os balões formam a conversão em texto, sendo reconhecidos para transcrição no formato tabular e estruturada em planilhas Excel.

<img width="1024" height="768" alt="663334581_1561169256017279_4272855674869920448_n" src="https://github.com/user-attachments/assets/31dd90fc-26e4-4a0d-a7b8-d601f4396d9b" />

Com sua evolução, essa ferramenta uniu o melhor dos dois mundos, pela imagem, o fluxo mostrado é:

![Fruzzy process](https://github.com/user-attachments/assets/f7c24204-a07b-4371-9c13-b0d04ada8c65)

1. Receber um PDF.
2. Fazer parsing das páginas.
3. Executar OCR (extração de texto de imagens).
4. Realizar análise de layout (entender títulos, tabelas, blocos, gráficos etc.).
5. Identificar a estrutura das tabelas.
6. Aplicar pós-processamento.
7. Serializar o resultado em JSON ou Markdown.

Ou seja, o que a imagem descreve é muito mais um sistema de **Document AI**, **Document Processing** ou **Intelligent Document Parsing** do que uma ferramenta específica para automação de planilhas.

Portanto, a automação e análise de planilhas faria mais sentido para ferramentas focadas em Excel, CSV, Google Sheets ou BI. Pela arquitetura mostrada, o Fruzzy parece estar mais próximo de soluções como [Apache Tika](https://tika.apache.org?utm_source=chatgpt.com), [Docling](https://docling-project.github.io/docling/?utm_source=chatgpt.com), [Unstructured.io](https://unstructured.io?utm_source=chatgpt.com) ou pipelines de OCR para ingestão de documentos. Isso pode ser usado em Engenharia de Dados, mas o foco principal parece ser **extração e estruturação de dados de documentos PDF**.

No entanto, devido a necessidade e complexidade dessas ferramentas acima em visão de um mundo onde a produtividade é baseada pela performance, ele precisa não somente extrair dados, mas sim produzir e mesclar dados em larga escala para produção (criação de aplicações consumidoras desses dados, como um produto ou ativo). Isso aqui sim é um diferencial conceitual claro, porque você está saindo do paradigma clássico de ETL/ELT (extrair, transformar, carregar) e indo para algo mais próximo de data generation pipelines ou data synthesis systems, o que conversa bastante com IA moderna, agentes e automação de conhecimento.

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

Extensões para o visual Studio:
- Conveyor 

## [Fruzzy] Endereço para criação de novos usuários 
- Para aplicações em desenvolvimento a criação de novos usuários deve acontecer neste endereço -> https://localhost:44312/Identity/Account/Register  
- Para aplicações em produção, a criação de novos usuários deve acontecer neste endereço -> https://fruzzydoc:45455/Identity/Account/Register 

## [Fruzzy] Base de dados e contexto 
O sistema está implementado no formato Database-First, não sendo necessário script para a criação do banco de dados. Apenas a inicialização do sistema com a variável de ambiente no modo de desenvolvimento. Ele irá apresentar a tela de login e escrever um email e senha 
aleatório. Ele apresentará a mensagem de ausência do banco de dados e apresentará uma mensagem de aplicar migration. Escolha a opção com a lista de maior opção. 

Em produção o sistema utiliza um banco de dados em sqlexpress com o banco, já configurado com a autenticação do Windows do sistema AD Fruzzy.  

## [Fruzzy] Bases em JSONs 
Existe uma pasta com um conjunto de arquivos nos formatos json. Eles tem as configurações de vários parâmetros para a leitura e identificação dos documentos que serão lidos. O apontamento do caminho da pasta leitura desses arquivos no formato json, deve ser feito no arquivo `appsettings.json` no parâmetro expressionjsons. 

## [Fruzzy] Formatação de datas 
Por conta da inteligência de leitura de datas da Microsoft, foi necessário “desserializar” as datas em arquivos separados pelo nome do arquivo no formato JSON. O apontamento do caminho da pasta de escrita desses arquivos no formato json, deve ser feito no arquivo `appsettings.json`. 

## [Fruzzy] Operations 
É permitido apenas uma operação em andamento por vez no sistema para garantirmos que não teremos “estouros” de memória por conta de um problema de alto processamento. Caso a operação esteja presa e o sistema com baixo processamento (abaixo de 6%), pode ser executado este comando para limpar alguma operação que tenha ficado presa. 

```sql
Update dbo.Operation set ready = ‘true’ Where ready = ‘false’. 
```

## [Fruzzy] Fuzzyficação
A **fuzzificação** é o núcleo lógico do Fruzzy para lidar com documentos do mundo real, que raramente seguem um padrão rígido e previsível. Diferente de sistemas baseados apenas em parsing determinístico, o Fruzzy parte do princípio de que PDFs escaneados, OCRizados ou convertidos para TXT apresentam variações de escrita, erros de reconhecimento óptico, diferenças de layout, abreviações, caracteres trocados e até mudanças sutis de vocabulário entre documentos que, semanticamente, representam a mesma informação.

No Fruzzy, a fuzzificação não termina no cálculo de similaridade textual. Embora o sistema utilize métricas fuzzy para lidar com imprecisão, ruídos de OCR e variações semânticas, a decisão final sobre o que é ou não um dado válido passa obrigatoriamente por um processo de crispificação, utilizando a função Crisp. Esse modelo híbrido foi adotado para garantir previsibilidade, consistência e segurança na extração de dados que, ao final do fluxo, precisam ser determinísticos para escrita em banco de dados e geração de planilhas Excel.

No contexto do sistema, a fuzzificação é o processo de transformar textos brutos extraídos via OCR em representações comparáveis, tolerantes a erro, permitindo identificar campos, rótulos, valores e estruturas mesmo quando não há correspondência exata entre o texto esperado e o texto efetivamente lido. Isso é feito utilizando similaridade textual, distância de edição e métricas probabilísticas, em vez de simples comparações por igualdade.

O Fruzzy utiliza a biblioteca <a href="">FuzzySharp</a>, que implementa algoritmos baseados principalmente na distância de Levenshtein, tokenização e ordenação de termos. Esses algoritmos permitem calcular um score de similaridade entre duas strings, variando normalmente de 0 a 100, onde valores mais altos indicam maior proximidade semântica. Na prática, isso permite que expressões como “Data de Emissão”, “Dt. Emissao”, “Data Emiss.” ou até “Data de Emissdo” (erro comum de OCR) sejam reconhecidas como o mesmo campo lógico dentro do documento.

O processo de fuzzificação no Fruzzy ocorre após a extração do texto bruto do PDF, seja ele oriundo de OCR direto (IronOCR / ASPOSE OCR) ou de leitura textual nativa do PDF. Antes de qualquer tentativa de mapeamento para colunas de planilha ou entidades de banco de dados, o texto passa por uma normalização básica, que inclui remoção de caracteres especiais irrelevantes, padronização de caixa, limpeza de espaços duplicados e segmentação por linhas ou blocos lógicos.

Em seguida, cada bloco de texto é comparado contra as expressões definidas nos arquivos de configuração JSON, que funcionam como uma base de conhecimento do sistema. Esses arquivos descrevem quais campos são esperados, quais sinônimos são aceitos, qual o limiar mínimo de similaridade para considerar uma correspondência válida e, em alguns casos, regras adicionais como posição relativa no documento ou proximidade com outros campos conhecidos. Esse mecanismo permite que o mesmo motor de fuzzificação seja reutilizado para diferentes tipos de documentos, bastando ajustar os parâmetros nos JSONs, sem necessidade de alterar código.

Um ponto crítico da fuzzificação no Fruzzy é o equilíbrio entre tolerância a erro e precisão. Limiar de similaridade muito baixo pode gerar falsos positivos, enquanto valores muito altos tornam o sistema rígido demais e incapaz de lidar com ruídos do OCR. Por isso, os thresholds de fuzzy matching foram definidos empiricamente, com base em documentos reais, e podem ser ajustados conforme o tipo de documento, qualidade do scan e idioma predominante. Em cenários de alto ruído, o sistema prioriza consistência estatística ao longo de múltiplos PDFs, em vez de decisões isoladas por documento.

A fuzzificação também é utilizada de forma complementar na interpretação de datas, valores monetários e identificadores textuais. Embora a `Microsoft.RecognizerText.DateTime` seja responsável pela interpretação semântica de datas, o Fruzzy utiliza fuzzy matching para identificar corretamente os rótulos associados a essas datas antes de delegar a interpretação ao motor cognitivo. Esse desacoplamento evita que datas sejam interpretadas fora de contexto ou associadas a campos incorretos.

Do ponto de vista de desempenho, a fuzzificação é uma das etapas mais custosas do pipeline, especialmente quando aplicada a centenas de documentos. Por isso, o sistema limita a execução a uma única operação por vez e utiliza estratégias de curto-circuito, como interrupção antecipada de comparações quando um score máximo aceitável já foi atingido. Essa abordagem reduz o consumo de memória e CPU sem comprometer a confiabilidade da extração.

Em resumo, a fuzzificação no Fruzzy não é apenas um detalhe de implementação, mas um princípio arquitetural. Ela permite que o sistema funcione em ambientes imperfeitos, com documentos inconsistentes e dados ruidosos, aproximando o processamento automatizado da forma como um humano interpreta informação textual: por similaridade, contexto e probabilidade, e não por igualdade absoluta. É esse mecanismo que torna viável a análise automatizada de centenas de PDFs heterogêneos e a consolidação confiável dos dados em planilhas Excel únicas e estruturadas.
