/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package lsb;

import java.awt.image.BufferedImage;
import java.io.File;
import javax.imageio.ImageIO;

/**
 *
 * @author TatTrung
 */
public class LSB {

    public static int[] getBitFromContent(String msg) {
        int j = 0;
        int[] b_msg = new int[msg.length() * 8];
        for (int i = 0; i < msg.length(); i++) {
            int x = msg.charAt(i);
            String x_s = Integer.toBinaryString(x);
            while (x_s.length() != 8) {
                x_s = '0' + x_s;
            }

            for (int i1 = 0; i1 < 8; i1++) {
                b_msg[j] = Integer.parseInt(String.valueOf(x_s.charAt(i1)));
                j++;
            };
        }

        return b_msg;
    }

    public static void hideTheMessage(int[] bits, BufferedImage theImage) throws Exception {
        File f = new File(".\\images\\image_encrypted.png");
        int bit_l = bits.length / 8;
        int[] bl_msg = new int[8];
        String bl_s = Integer.toBinaryString(bit_l);
        while (bl_s.length() != 8) {
            bl_s = '0' + bl_s;
        }
        for (int i1 = 0; i1 < 8; i1++) {
            bl_msg[i1] = Integer.parseInt(String.valueOf(bl_s.charAt(i1)));
        };
        int j = 0;
        int b = 0;
        int currentBitEntry = 8;

        for (int x = 0; x < theImage.getWidth(); x++) {
            for (int y = 0; y < theImage.getHeight(); y++) {
                if (x == 0 && y < 8) {
                    int currentPixel = theImage.getRGB(x, y);
                    int red = currentPixel >> 16;
                    red = red & 255;
                    int green = currentPixel >> 8;
                    green = green & 255;
                    int blue = currentPixel;
                    blue = blue & 255;
                    String x_s = Integer.toBinaryString(blue);
                    String sten_s = x_s.substring(0, x_s.length() - 1);
                    sten_s = sten_s + Integer.toString(bl_msg[b]);

                    int temp = Integer.parseInt(sten_s, 2);
                    int s_pixel = Integer.parseInt(sten_s, 2);
                    int a = 255;
                    int rgb = (a << 24) | (red << 16) | (green << 8) | s_pixel;
                    theImage.setRGB(x, y, rgb);
                    ImageIO.write(theImage, "png", f);
                    b++;
                } else if (currentBitEntry < bits.length + 8) {
                    int currentPixel = theImage.getRGB(x, y);
                    int red = currentPixel >> 16;
                    red = red & 255;
                    int green = currentPixel >> 8;
                    green = green & 255;
                    int blue = currentPixel;
                    blue = blue & 255;
                    String x_s = Integer.toBinaryString(blue);
                    String sten_s = x_s.substring(0, x_s.length() - 1);
                    sten_s = sten_s + Integer.toString(bits[j]);
                    j++;
                    int temp = Integer.parseInt(sten_s, 2);
                    int s_pixel = Integer.parseInt(sten_s, 2);

                    int a = 255;
                    int rgb = (a << 24) | (red << 16) | (green << 8) | s_pixel;
                    theImage.setRGB(x, y, rgb);
                    ImageIO.write(theImage, "png", f);

                    currentBitEntry++;
                }
            }
        }
    }

    public static String decodeTheMessage(BufferedImage yImage) throws Exception {
        int len = 0;

        String b_msg = "";

        int currentBitEntry = 0;
        String bx_msg = "";
        for (int x = 0; x < yImage.getWidth(); x++) {
            for (int y = 0; y < yImage.getHeight(); y++) {
                if (x == 0 && y < 8) {
                    int currentPixel = yImage.getRGB(x, y);
                    int red = currentPixel >> 16;
                    red = red & 255;
                    int green = currentPixel >> 8;
                    green = green & 255;
                    int blue = currentPixel;
                    blue = blue & 255;
                    String x_s = Integer.toBinaryString(blue);
                    bx_msg += x_s.charAt(x_s.length() - 1);
                    len = Integer.parseInt(bx_msg, 2);
                } else if (currentBitEntry < len * 8) {
                    int currentPixel = yImage.getRGB(x, y);
                    int red = currentPixel >> 16;
                    red = red & 255;
                    int green = currentPixel >> 8;
                    green = green & 255;
                    int blue = currentPixel;
                    blue = blue & 255;
                    String x_s = Integer.toBinaryString(blue);
                    b_msg += x_s.charAt(x_s.length() - 1);

                    currentBitEntry++;
                }
            }
        }

        String msg = "";
        for (int i = 0; i < len * 8; i = i + 8) {
            String sub = b_msg.substring(i, i + 8);
            int m = Integer.parseInt(sub, 2);
            char ch = (char) m;
            msg += ch;
        }
        return msg;
    }
}
