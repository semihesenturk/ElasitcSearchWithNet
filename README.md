ElasitcSearchWithNet

Bu proje, .NET Core kullanarak Elasticsearch işlemlerini gerçekleştiren bir örnek uygulamadır. İki farklı branch bulunmaktadır:

nest: Eski Elasticsearch kütüphanesi olan NEST kullanılarak yapılan işlemleri içerir.

elastic-client: Yeni Elasticsearch kütüphanesi olan Elastic.Clients.Elasticsearch kullanılarak yapılan işlemleri içerir.

📌 Proje İçeriği

Elasticsearch kurulumu ve yapılandırması

Veri ekleme, güncelleme, silme işlemleri

Query DSL ile gelişmiş arama sorguları

Elasticsearch indeks yönetimi

🚀 Kurulum

Depoyu klonlayın

git clone https://github.com/semihesenturk/ElasitcSearchWithNet.git
cd ElasitcSearchWithNet

Gerekli bağımlılıkları yükleyin

dotnet restore

Elasticsearch'i başlatın (Eğer Docker kullanıyorsanız):

docker run -d -p 9200:9200 -e "discovery.type=single-node" elasticsearch:8.5.1

Projeyi çalıştırın

dotnet run

🛠 Kullanım

Nest Branch: Eski NEST kütüphanesi ile Elasticsearch işlemleri

Elastic-Client Branch: Yeni Elastic.Clients.Elasticsearch kütüphanesi ile modern Elasticsearch işlemleri

Branch değiştirmek için aşağıdaki komutu kullanabilirsiniz:

git checkout nest   # Eski versiyon için

git checkout elastic-client   # Yeni versiyon için

📄 Kaynaklar

Elasticsearch Resmi Dokümantasyon

NEST Kütüphanesi

Elastic.Clients.Elasticsearch Kütüphanesi

🤝 Katkıda Bulunma

Katkıda bulunmak için lütfen bir pull request açmadan önce bir issue oluşturun. Geri bildiriminiz değerlidir! 🎉

📌 Lisans

Bu proje MIT Lisansı ile lisanslanmıştır. Detaylar için LICENSE dosyasına göz atabilirsiniz.

Proje ile ilgili sorularınız için bana ulaşabilirsiniz! 😊
