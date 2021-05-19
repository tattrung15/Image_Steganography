FID = fopen('myfile.txt', 'rb');    %opening text file
Str = fread(FID, [1, inf], 'char'); %reading text file
fclose(FID);        %closing the file
Str=uint16(Str);    %converting to 16 bit numbers for proper calculation

x=imread('Harry.jpg');   %reading the image file
x=uint16(x);                %conversion to 16 bit
[x_row,x_col]=size(x);

c=numel(Str);   %counting characters
a=1;

%encrypting loop
for i=1:x_row
    for j=1:x_col
        if(a<=c)
            
            if(x(i,j)+Str(a)>255)
                temp=x(i,j)+Str(a)-256;
            else
                temp=x(i,j)+Str(a);
            end
            z(i,j)=uint8(temp);
        else
            z(i,j)=uint8(x(i,j));
        end
        a=a+1;
    end
end

imwrite(z,'encrypted.png');     %writing the encrypted data as pixels in image

