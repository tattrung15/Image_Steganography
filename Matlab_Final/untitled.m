function varargout = untitled(varargin)
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @untitled_OpeningFcn, ...
                   'gui_OutputFcn',  @untitled_OutputFcn, ...
                   'gui_LayoutFcn',  [] , ...
                   'gui_Callback',   []);
if nargin && ischar(varargin{1})
    gui_State.gui_Callback = str2func(varargin{1});
end

if nargout
    [varargout{1:nargout}] = gui_mainfcn(gui_State, varargin{:});
else
    gui_mainfcn(gui_State, varargin{:});
end

function untitled_OpeningFcn(hObject, eventdata, handles, varargin)
handles.output = hObject;
guidata(hObject, handles);

img=imread('upt.png');
axes(handles.axes3);
imshow(img);

function varargout = untitled_OutputFcn(hObject, eventdata, handles) 
varargout{1} = handles.output;


%Chon anh tu file
function pushbutton1_Callback(hObject, eventdata, handles)
[filename1 pathname1]=uigetfile({'*.png;*.jpg'},'File Selector')
global fullpathname1
fullpathname1 = strcat(pathname1,filename1) 
set(handles.text3,'String',fullpathname1) 
img=imread(fullpathname1); 
axes(handles.axes1);
imshow(img); 

%chon file txt 
function pushbutton3_Callback(hObject, eventdata, handles)
[filename2 pathname2]=uigetfile({'*.txt'},'File Selector') 
global fullpathname2
fullpathname2 = strcat(pathname2,filename2) 
set(handles.text4,'String',fullpathname2) 
text = fileread(fullpathname2) 
set(handles.text5,'String',text) 

%Mã hóa tep
function pushbutton4_Callback(hObject, eventdata, handles)
global fullpathname1
global fullpathname2
FID = fileread(fullpathname2);       %Doc tep
Str=uint16(FID);    

x=imread(fullpathname1);   
x=uint16(x);                %Chuyen  sang  16bit
[x_row,x_col]=size(x); %Giá tr? piexel ?nh =matran x

c=numel(Str);   %dem so ký tu cua tin giau
a=1;

%Mã hóa
for i=1:x_row
    for j=1:x_col
        if(a<=c)      
            if(x(i,j)+Str(a)>255) %Neu tong các pixel hình anh,kí tu tin >255
                temp=x(i,j)+Str(a)-256;  %Tong giá tri pixel+Tong ký tu -256
            else
                temp=x(i,j)+Str(a); 
            end
            z(i,j)=uint8(temp); %ket qua quá trình vào ma tran hình anh moi
        else
            z(i,j)=uint8(x(i,j));
        end
        a=a+1;
    end
end
imwrite(z,'encrypted.png');      %Ghi hình anh duoc mã hóa lên màn hình
img=imread('encrypted.png');    
axes(handles.axes2);    %thiet lap các tr?c ?? hien thi hình ?nh.
imshow(img);    

% Giai mã hình anh sau khi mã hóa
function pushbutton5_Callback(hObject, eventdata, handles)
x=imread('encrypted.png');  
global fullpathname1
y=imread(fullpathname1);   

x=uint16(x);   
y=uint16(y);  

[x_row, x_col]=size(x);  %gan so hang,cot cua anh

b=0;k=1;

% Giai mã vòng lap
for i=1:x_row
    for j=1:x_col
        
        if(x(i,j)>=y(i,j))  %neu gia tri pixel anh ma hoa >anh goc
            a=x(i,j)-y(i,j); 
        else
            a=256+x(i,j)-y(i,j); 
        end
        
        if(a~=0)
            z(k)=uint8(a); %Gan ma tran moi =matran a chuyen sang kieu uint(8)
            k=k+1;
        else
            b=1;
            break;
        end
    end
    if(b==1)
        break;
    end
end
% tao file tách tin
fid=fopen('decrypted.txt','w'); %ghi file
for i=1:k-1
    fprintf(fid,'%c',z(i));
end

text = fileread('decrypted.txt')
set(handles.text6,'String',text)
