#include<stdio.h>
#include<string.h>

typedef struct bignumber
{
    //整數部分、小數部分
    int ii[130],fl[130];
    //整數長度、小數長度
    int l1,l2;
    //正負號
    int fd;
}big;

//加
add(big *ou,big b1,big b2)
{
    int i,j;
    if(b1.fd==-1)
    {
        for(i=0;i<b1.l1;i++)
            b1.ii[i]*=-1;
        for(i=0;i<b1.l2;i++)
            b1.fl[i]*=-1;
    }
    if(b2.fd==-1)
    {
        for(i=0;i<b2.l1;i++)
            b2.ii[i]*=-1;
        for(i=0;i<b2.l2;i++)
            b2.fl[i]*=-1;
    }

    //清空資料
    int max1=b1.l1>b2.l1?b1.l1:b2.l1;
    int max2=b1.l2>b2.l2?b1.l2:b2.l2;
    (*ou).l1=0;
    (*ou).l2=0;
    for(i=0;i<130;i++)
        (*ou).ii[i]=0;
    for(i=130-1;i>=0;i--)
        (*ou).fl[i]=0;

    for(i=max2-1;i>=0;i--)
    {
        if(i<b1.l2)
            (*ou).fl[i]+=b1.fl[i];
        if(i<b2.l2)
            (*ou).fl[i]+=b2.fl[i];
        if((*ou).fl[i]>=10)
        {
            (*ou).fl[i]-=10;
            if(i!=0)
                (*ou).fl[i-1]++;
            else
                (*ou).ii[0]++;
        }
        else if((*ou).fl[i]<=-10)
        {
            (*ou).fl[i]+=10;
            if(i!=0)
                (*ou).fl[i-1]--;
            else
                (*ou).ii[0]--;
        }
        if((*ou).fl[i]!=0 && i+1>(*ou).l2)
            (*ou).l2=i+1;
    }
    for(i=0;i<max1;i++)
    {
        if(i<b1.l1)
            (*ou).ii[i]+=b1.ii[i];
        if(i<b2.l1)
            (*ou).ii[i]+=b2.ii[i];
        if((*ou).ii[i]>=10)
        {
            (*ou).ii[i]-=10;
            (*ou).ii[i+1]++;
            if((*ou).ii[i+1]!=0&&i+2>(*ou).l1)
                (*ou).l1=i+2;
        }
        else if((*ou).ii[i]<=-10)
        {
            (*ou).ii[i]+=10;
            (*ou).ii[i+1]--;
            if((*ou).ii[i+1]!=0&&i+2>(*ou).l1)
                (*ou).l1=i+2;
        }
        if((*ou).ii[i]!=0&&i+1>(*ou).l1)
            (*ou).l1=i+1;
    }
    (*ou).fd=(*ou).ii[(*ou).l1-1]>=0?1:-1;
    if((*ou).fd==1)
    {
        for(i=(*ou).l2-1;i>=0;i--)
        {
            if((*ou).fl[i]<0)
            {
                (*ou).fl[i]+=10;
                if(i!=0)
                    (*ou).fl[i-1]--;
                else
                    (*ou).ii[0]--;
            }
        }
        for(i=0;i<(*ou).l1;i++)
            if((*ou).ii[i]<0)
            {
                (*ou).ii[i]+=10;
                (*ou).ii[i+1]--;
            }
    }
    else
    {
        //printf("負數\n");
        for(i=(*ou).l2-1;i>=0;i--)
        {
            if((*ou).fl[i]>0)
            {
                (*ou).fl[i]-=10;
                if(i!=0)
                    (*ou).fl[i-1]++;
                else
                    (*ou).ii[0]++;
            }
        }
        for(i=(*ou).l2-1;i>=0;i--)
            (*ou).fl[i]*=-1;
        for(i=0;i<(*ou).l1;i++)
            if((*ou).ii[i]>0)
            {
                (*ou).ii[i]-=10;
                (*ou).ii[i+1]++;
            }
        for(i=0;i<(*ou).l1;i++)
            (*ou).ii[i]*=-1;
    }

    //抓數值最大長度
    int max_l=0;
    for(i=0;i<130;i++)
        if((*ou).ii[i]!=0&&i>max_l)
            max_l=i;
    (*ou).l1=max_l+1;
    max_l=-1;
    for(i=0;i<130;i++)
        if((*ou).fl[i]!=0&&i>max_l)
            max_l=i;
    (*ou).l2=max_l+1;
}

//乘
mul(big *ou,big b1,big b2)
{
    (*ou).fd=b1.fd*b2.fd;
    int i,j;
    for(i=0;i<130;i++)
        (*ou).ii[i]=0;
    for(i=130-1;i>=0;i--)
        (*ou).fl[i]=0;

    //小數運算
    for(i=b1.l2-1;i>=0;i--)
    {
        for(j=b2.l2-1;j>=0;j--)
        {
            (*ou).fl[i+j+1]+=b1.fl[i]*b2.fl[j];
            //printf("i:%d, j:%d, ou.fl:%d\n",i,j,(*ou).fl[i+j+1]);
            while((*ou).fl[i+j+1]>=10)
            {
                (*ou).fl[i+j+1]-=10;
                if(i+j+1-1>=0)
                    (*ou).fl[i+j+1-1]++;
                else
                    (*ou).ii[0]++;
            }
        }
        //小數配整數
        for(j=0;j<b2.l1;j++)
        {
            if(j>i)
            {
                (*ou).ii[j-i-1]+=b1.fl[i]*b2.ii[j];
                while((*ou).ii[j-i-1]>=10)
                {
                    (*ou).ii[j-i-1]-=10;
                    (*ou).ii[j-i-1+1]++;
                }
            }
            else
            {
                (*ou).fl[i-j]+=b1.fl[i]*b2.ii[j];
                while((*ou).fl[i-j]>=10)
                {
                    (*ou).fl[i-j]-=10;
                    if(i-j-1>=0)
                        (*ou).fl[i-j-1]++;
                    else
                        (*ou).ii[0]++;
                }
            }
        }
    }

    //整數運算
    for(i=0;i<b1.l1;i++)
    {
        for(j=0;j<b2.l1;j++)
        {
            (*ou).ii[i+j]+=b1.ii[i]*b2.ii[j];
            while((*ou).ii[i+j]>=10)
            {
                (*ou).ii[i+j]-=10;
                (*ou).ii[i+j+1]++;
            }
        }
        //整數配小數
        for(j=b2.l2-1;j>=0;j--)
        {
            if(i>j)
            {
                (*ou).ii[i-j-1]+=b1.ii[i]*b2.fl[j];
                while((*ou).ii[i-j-1]>=10)
                {
                    (*ou).ii[i-j-1]-=10;
                    (*ou).ii[i-j-1+1]++;
                }
            }
            else
            {
                (*ou).fl[j-i]+=b1.ii[i]*b2.fl[j];
                while((*ou).fl[j-i]>=10)
                {
                    (*ou).fl[j-i]-=10;
                    if(j-i-1>=0)
                        (*ou).fl[j-i-1]++;
                    else
                        (*ou).ii[0]++;
                }
            }
        }
    }

    //小數修正
    (*ou).l2=b1.l2+b2.l2;
    for(i=129;i>=0;i--)
        while((*ou).fl[i]>=10)
        {
            (*ou).fl[i]-=10;
            if(i!=0)
                (*ou).fl[i-1]++;
            else
                (*ou).ii[0]++;
        }

    //整數修正
    (*ou).l1=b1.l1+b2.l1;
    for(i=0;i<130;i++)
        while((*ou).ii[i]>=10)
        {
            (*ou).ii[i]-=10;
            (*ou).ii[i+1]++;
        }

    //抓數值最大長度
    int max_l=-1;
    for(i=130-1;i>=0;i--)
        if((*ou).fl[i]!=0&&i>max_l)
            max_l=i;
    (*ou).l2=max_l+1;

    //抓數值最大長度
    max_l=-1;
    for(i=0;i<130;i++)
        if((*ou).ii[i]!=0&&i>max_l)
            max_l=i;
    (*ou).l1=max_l+1;
}

//確認範圍內是否有數值
int ck(int cc[],int l)
{
    int i;
    for(i=l-1;i>=0;i--)
        if(cc[i]!=0)
            return i+1;
    return -1;
}

main()
{
    //清空
    int n,i,j;
    big b1,b2,ou;
    for(i=0;i<130;i++)
        b1.ii[i]=0;
    for(i=130-1;i>=0;i--)
        b1.fl[i]=0;
    for(i=0;i<130;i++)
        b2.ii[i]=0;
    for(i=130-1;i>=0;i--)
        b2.fl[i]=0;

    //使用者輸入
    char c1[200],c2[200];
    int l1,l2;
    scanf("%s",c1);
    scanf("%s",c2);
    scanf("%d",&n);
    l1=strlen(c1);
    l2=strlen(c2);

    //b1的數值
    i=0;
    if(c1[0]=='-')
    {
        b1.fd=-1;
        i++;
    }
    else
        b1.fd=1;
    for(;i<l1;i++)
    {
        if(c1[i]=='.')
        {
            b1.l1=i;
            if(b1.fd==-1)
                b1.l1--;
            b1.l2=l1-i-1;
            //抓整數
            for(j=0;j<b1.l1;j++)
                b1.ii[j]=c1[i-j-1]-'0';
            //抓小數
            for(j=i+1;j<l1;j++)
                b1.fl[j-(i+1)]=c1[j]-'0';
            break;
        }
    }

    //b2的數值
    i=0;
    if(c2[0]=='-')
    {
        b2.fd=-1;
        i++;
    }
    else
        b2.fd=1;
    for(i=0;i<l2;i++)
    {
        if(c2[i]=='.')
        {
            b2.l1=i;
            if(b2.fd==-1)
                b2.l1--;
            b2.l2=l2-i-1;
            //抓整數
            for(j=0;j<b2.l1;j++)
                b2.ii[j]=c2[i-j-1]-'0';
            //抓小數
            for(j=i+1;j<l2;j++)
                b2.fl[j-(i+1)]=c2[j]-'0';
            break;
        }
    }

    //加
    add(&ou,b1,b2);
    if(ou.fd==-1)
        printf("-");
    for(i=ou.l1-1;i>=0;i--)
        printf("%d",ou.ii[i]);
    int n2=ck(ou.fl,n);
    if(ou.l2!=0&& n2!=-1)
    {
        printf(".");
        for(i=0;i<n2;i++)
            printf("%d",ou.fl[i]);
    }
    printf("\n");

    //減
    b2.fd*=-1;
    add(&ou,b1,b2);
    b2.fd*=-1;
    if(ou.fd==-1)
        printf("-");
    for(i=ou.l1-1;i>=0;i--)
        printf("%d",ou.ii[i]);
    n2=ck(ou.fl,n);
    if(ou.l2!=0&& n2!=-1)
    {
        printf(".");
        for(i=0;i<n2;i++)
            printf("%d",ou.fl[i]);
    }
    printf("\n");

    //乘
    mul(&ou,b1,b2);
    if(ou.fd==-1)
        printf("-");
    if(ou.l1==0)
        printf("0");
    else
        for(i=ou.l1-1;i>=0;i--)
            printf("%d",ou.ii[i]);
    n2=ck(ou.fl,n);
    if(ou.l2!=0&& n2!=-1)
    {
        printf(".");
        for(i=0;i<n2;i++)
            printf("%d",ou.fl[i]);
    }

/*
    printf("\nb1.l1:%d, b1.l2:%d\n",b1.l1,b1.l2);
    printf("b2.l1:%d, b2.l2:%d, b2[l2-1]:%d\n",b2.l1,b2.l2,b2.fl[b2.l2-1]);
    printf("ou.l1:%d, ou.l2:%d\n",ou.l1,ou.l2);
    //return;*/
}
