# Catalogue Formateurs Associés

# deploy with jenkins :
## start in local with docker use this cmd :

docker-compose --env-file ./.env -f docker-compose-local.yml up  -d

## for cicd :

you can see job with this [url](https://jenkins.smartbe.be/view/github/job/smart-cfa/).

when you push your branch jenkins read Jenkinsfile and execute jenkins-deploy.sh
For ne secret key jenkins use this credentials [url](https://jenkins.smartbe.be/view/github/job/smart-cfa/credentials/store/folder/domain/_/)

for branch feature/xxx use url feature-xxx.cfa.smartcoop.dev
for branch  xxx use url xxx.cfa.smartcoop.dev
