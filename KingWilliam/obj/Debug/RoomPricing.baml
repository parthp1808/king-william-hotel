   M S B A M L   `   `   `  ����F  BKingWilliam, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:KingWilliamKingWilliam  5   	       KingWilliam.RoomPricing   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:KingWilliam     5   	     � Title$  RoomPricing��5   	   $��904.654��6   $��1545.593��6.    � 
ResizeMode$ CanMinimize=�6?    � WindowState$ 	Maximized=�6X   .��5	      � .+�5
   
   �� $��0 0 0 1q�6   ���	#FF3556C8$
��Left=�6D   $	��119��6_   $	��Top=�6l   $
��1534��6�   ._�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      �� -   5         ��Name$ image��5      $
��Left=�6'   $��96��6B   $��	57,10,0,0q�6N   $	��Top=�6a   $��86��6y   $��Images/logo.jpg��6�    J� RenderTransformOrigin$ 0.437,0.013&�6�   �� 5      -   5         ��Name$ label��5      $��King William Hotel��6'   $��Center=�6D   $��
149,10,0,0q�6a   $	��Top=�6u   $��96��6�   $	��614��6�   $��68(�6�   $��Old English Text MT)�6�   ���	#FFA5A1816�   5      5      �� 5   
   -   5        $ lblPricingTitle��5      $��Room Pricing��6)   $
��Left=�6@   $��170,119,0,0q�6[   $	��Top=�6p   $��48(�6�   $
��Bold"�6�   �� 5   
   -   5        $ label1��5      $��
Room Type:��6    $
��Left=�65   $��451,327,0,0q�6P   $	��Top=�6e   $��24(�6}   �� 5   
   -   5         ��Name$ cmbRoomType��5      $
��Left=�6(   $��794,327,0,0q�6C   $	��Top=�6X   $	��209��6p   $��42��6|   6�   �� 5   
   -   5        $��
cnvPricing��5   .   $��451,421,533.2,303.2q�6   .+�5      �� -   5         ��Name$ 	btnCancel��5      $��Cancel��6(   $
��Left=�69   $	��Top=�6T   $	��212��6l   $��55��6x   ���	#FF9A6836$��18(�6�   ���	#FFF1F1F1	 �� Left$		 343��6�   

 �� Top$
 96��6�   6�   �� 5      -   5        $ 	btnUpdate��5   ,   $y�	Collapsed=�6   $��Change Price��6?   $
��Left=�6V   $	��Top=�6q   $	��211��6�   $��55��6�   ���	#FF9A6836$��18(�6�   ���	#FFF1F1F1$
 96��6�   6�   �� 5      -	   5        $ lblPrice��5      $��Price:��6&   $
��Left=�67   $	��Top=�6R   $��24(�6j   �� 5      -
   5         ��Name$ txtPrice��5      $��32��6(   $		 343��64    �� TextWrapping$
 Wrap=�6F   $�� ��6Z   $
 10��6b   $	��202��6r   $��16(�6~   5      �� 5   
   -   5        $��cnvRoomNumber��5   .   $��451,426,536.2,407.2q�6   $y�	Collapsed=�6C   .+�5      �� -   5        $ lblRoomNumber��5      $��Room Number:��6+   $
��Left=�6B   $	��Top=�6]   $��24(�6u   �� 5      -   5        $ cmbRoomNumber��5      $
��Left=�6.   $	��Top=�6I   $	��209��6a   $��42��6m   $		 343��6y   6�   5       5#      5$      